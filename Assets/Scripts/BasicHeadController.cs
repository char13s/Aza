using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.Playables;

//For Unity 2019.2 and below:


[RequireComponent(typeof(Animator))]
public class BasicHeadController : MonoBehaviour
{

    private struct HeadData : IAnimationJob
    {
        public TransformStreamHandle root;
        public TransformStreamHandle head;
        public TransformSceneHandle headTarget;
        public Vector3 localRootUp;
        public Vector3 localOffsetEulerAngles;

        public void ProcessRootMotion(AnimationStream stream) { }
        public void ProcessAnimation(AnimationStream stream)
        {
            Vector3 rootUp = root.GetRotation(stream) * localRootUp; //In world-space
            Vector3 targetPos = headTarget.GetPosition(stream);
            Vector3 headPos = head.GetPosition(stream);
            Vector3 delta = targetPos - headPos;

            //This had roll issues
            //head.SetRotation(stream, Quaternion.FromToRotation(forwardAxis, delta));

            //Use this instead!
            head.SetRotation(stream, Quaternion.LookRotation(delta, rootUp) * Quaternion.Euler(localOffsetEulerAngles));
        }
    }

    [SerializeField] private Vector3 localRootUp = new Vector3(0, 1, 0);

    [Space(20)]
    [SerializeField] private Transform root;
    [SerializeField] private Transform head;
    [SerializeField] private Transform headTarget;
    [SerializeField] private GameObject target;
    //[Tooltip("The axis of the head bone that will face towards the head target.")]
    //[SerializeField] private Vector3 headForward = new Vector3(0, 0, 1);
    [SerializeField] private Vector3 localOffsetEulerAngles = new Vector3(0, 0, 0);
    [Range(0, 1)]
    [SerializeField] private float weight = 1;

    [Tooltip("Forces delay on creating the graph, in case any strange animator behaviour" +
        "arrises when the graph is created immediately during OnEnable.")]
    [SerializeField] private int forceWaitFrames = 0;




    [Space(40)]
    [Tooltip("If this doesn't work due to strange 3D model and Avatar limitations, transform orientations messing up, etc." +
        "\n\nWould you like to force a last-ditch effort to use LateUpdate instead?")]
    [SerializeField] private bool lastResortLateUpdate = false;





    private Animator animator;
    private PlayableGraph graph;
    private AnimationPlayableOutput animOutput;
    private AnimationScriptPlayable script;

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public void OnValidate()
    {
        forceWaitFrames = Mathf.Max(0, forceWaitFrames);
    }

    public void Awake()
    {
        
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        headTarget = target.transform;
    }
    public void OnEnable()
    {
        if (lastResortLateUpdate)
            return;
        if (forceWaitFrames <= 0)
            CreateGraph();
        else
            StartCoroutine(PerformAfterWaitFrames(forceWaitFrames, CreateGraph));
    }

    public void OnDisable()
    {
        if (graph.IsValid())
            graph.Destroy();
    }

    private IEnumerator PerformAfterWaitFrames(int framesToWait, UnityAction action)
    {
        for (int i = 0; i < framesToWait; i++)
            yield return null;
        action();
    }

    private void CreateGraph()
    {
        if (head == null)
        {
            Debug.LogError("Head bone must be assigned. Unable to create graph.");
            return;
        }
        if (headTarget == null)
        {
            Debug.LogError("Head target must be assigned. Unable to create graph.");
            return;
        }
        if (root == null)
        {
            Debug.LogError("Root must be assigned. Unable to create graph.");
            return;
        }
        animator.avatar = AvatarBuilder.BuildGenericAvatar(gameObject, name);
        animator.Rebind();
        graph = PlayableGraph.Create(GetType().Name + ": " + name);
        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        animOutput = AnimationPlayableOutput.Create(graph, "Anim Output", animator);

        HeadData jobData = new HeadData()
        {
            root = animator.BindStreamTransform(root),
            head = animator.BindStreamTransform(head),
            headTarget = animator.BindSceneTransform(headTarget),
            localRootUp = localRootUp,
            localOffsetEulerAngles = localOffsetEulerAngles
        };
        script = AnimationScriptPlayable.Create(graph, jobData, 1);
        animOutput.SetSourcePlayable(script);

        AnimatorControllerPlayable animCtrl = AnimatorControllerPlayable.Create(graph, animator.runtimeAnimatorController);
        graph.Connect(animCtrl, 0, script, 0);
        script.SetInputWeight(0, 1);

        graph.Play();
    }

    public void Update()
    {
        if (animOutput.IsOutputValid())
        {
            animOutput.SetWeight(weight);
        }
        if (script.IsValid())
        {
            HeadData d = script.GetJobData<HeadData>();
            d.localRootUp = localRootUp;
            d.localOffsetEulerAngles = localOffsetEulerAngles;
            script.SetJobData(d);
        }
    }

    public void LateUpdate()
    {
        if (lastResortLateUpdate)
        {
            if (root != null && head != null && headTarget != null)
            {
                Vector3 rootUp = root.rotation * localRootUp;
                Vector3 targetPos = headTarget.position;
                Vector3 headPos = head.position;
                Vector3 delta = targetPos - headPos;

                head.rotation = Quaternion.Lerp(head.rotation, Quaternion.LookRotation(delta, rootUp) * Quaternion.Euler(localOffsetEulerAngles), weight);
            }
        }
    }
}