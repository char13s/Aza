using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;
public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private TimelineAsset timelineAsset;
    [SerializeField] private GameObject timelineObject;
    public static event UnityAction<EnemyTimelineTriggers> sendTrigger;
    public static event UnityAction<int> disableCodeMove;
    //public static event UnityAction demonKnightTakedown;
    // Start is called before the first frame update
    void Start() {
        timelineAsset = (TimelineAsset)timeline.playableAsset;
        //var track = (TrackAsset)timelineAsset.outputs[i].sourceObject;
        //Debug.Log(timeline.SetGenericBinding(timelineAsset.GetOutputTrack(1), Player.GetPlayer());
        //timeline.SetGenericBinding(timelineAsset.GetOutputTrack(1), Player.GetPlayer().Anim);
        //timeline.SetGenericBinding(timelineAsset.GetOutputTrack(3), Player.GetPlayer().TopAnim);
        //timeline.SetGenericBinding(timelineAsset.GetOutputTrack(4), Player.GetPlayer().BattleMode);
    }

    private void OnTriggerEnter(Collider other) {
        //parent player to timeline object
        disableCodeMove.Invoke(3);
        timeline.transform.position=Player.GetPlayer().transform.position;
        Player.GetPlayer().transform.SetParent(timelineObject.transform);
        timeline.Play();
    }
    public void End() {
        print("End of the timeline");
        Player.GetPlayer().transform.SetParent(Player.GetPlayer().transform);
    }
}
