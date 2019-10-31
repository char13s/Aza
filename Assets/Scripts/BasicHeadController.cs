using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class BasicHeadController : MonoBehaviour {
	private struct HeadData : IAnimationJob {
		public TransformStreamHandle head;
		public TransformSceneHandle headTarget;

		public void ProcessRootMotion(AnimationStream stream) { }
		public void ProcessAnimation(AnimationStream stream) {
			Vector3 targetPos = headTarget.GetPosition(stream);
			Vector3 headPos = head.GetPosition(stream);
			head.SetRotation(stream, Quaternion.LookRotation(targetPos - headPos, Vector3.up));
		}
	}

	[SerializeField] private Transform head;
	[SerializeField] private Transform headTarget;

	private Animator animator;
	private PlayableGraph graph;
	private AnimationScriptPlayable script;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnEnable() {
		if (head == null) {
			Debug.LogError("Head bone must be assigned. Unable to create graph.");
			return;
		}
		if (headTarget == null) {
			Debug.LogError("Head target must be assigned. Unable to create graph.");
			return;
		}
		graph = PlayableGraph.Create(GetType().Name + ": " + name);
		AnimationPlayableOutput animOutput = AnimationPlayableOutput.Create(graph, "Anim Output", animator);

		HeadData jobData = new HeadData() {
			head = animator.BindStreamTransform(head),
			headTarget = animator.BindSceneTransform(headTarget)
		};
		script = AnimationScriptPlayable.Create(graph, jobData);
		animOutput.SetSourcePlayable(script);
	}

	public void OnDisable() {
		if (graph.IsValid())
			graph.Destroy();
	}
}