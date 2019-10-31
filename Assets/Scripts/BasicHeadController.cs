using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.Playables;


//For Unity 2019.2 and below:
using UnityEngine.Experimental.Animations;


[RequireComponent(typeof(Animator))]
public class BasicHeadController : MonoBehaviour
{
    private struct HeadData : IAnimationJob
    {
        public TransformStreamHandle head;
        public TransformSceneHandle headTarget;
        public Vector3 forwardAxis;

        public void ProcessRootMotion(AnimationStream stream) { }
        public void ProcessAnimation(AnimationStream stream)
        {
            Vector3 targetPos = headTarget.GetPosition(stream);
            Vector3 headPos = head.GetPosition(stream);
            //head.SetRotation(stream, Quaternion.LookRotation(targetPos - headPos, Vector3.up));
            head.SetRotation(stream, Quaternion.FromToRotation(forwardAxis, targetPos - headPos));
        }
    }

    [SerializeField] private Transform head;
    [SerializeField] private Transform headTarget;
    [Tooltip("The axis of the head bone that will face towards the head target.")]
    [SerializeField] private Vector3 forwardAxis = new Vector3(0, 0, 1);
    [Range(0, 1)]
    [SerializeField] private float weight = 1;

    [Tooltip("Forces delay on creating the graph, in case any strange animator behaviour" +
        "arrises when the graph is created immediately during OnEnable.")]
    [SerializeField] private int forceWaitFrames = 0;

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
        headTarget = ThreeDCamera.Retical;
        animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
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
        graph = PlayableGraph.Create(GetType().Name + ": " + name);
        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        animOutput = AnimationPlayableOutput.Create(graph, "Anim Output", animator);

        HeadData jobData = new HeadData()
        {
            head = animator.BindStreamTransform(head),
            headTarget = animator.BindSceneTransform(headTarget),
            forwardAxis = forwardAxis
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
            d.forwardAxis = forwardAxis;
            script.SetJobData(d);
        }
    }
}