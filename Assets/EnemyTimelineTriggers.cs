using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;
public class EnemyTimelineTriggers : MonoBehaviour
{
    [SerializeField]private Enemy enemy;
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private TimelineAsset timelineAsset;
    public static event UnityAction<EnemyTimelineTriggers> sendTrigger;
    public static event UnityAction demonKnightTakedown;
    // Start is called before the first frame update
    void Start()
    {
        timelineAsset = (TimelineAsset)timeline.playableAsset;
        //var track = (TrackAsset)timelineAsset.outputs[i].sourceObject;
        //Debug.Log(timeline.SetGenericBinding(timelineAsset.GetOutputTrack(1), Player.GetPlayer());
        timeline.SetGenericBinding(timelineAsset.GetOutputTrack(2), Player.GetPlayer().Anim);
        timeline.SetGenericBinding(timelineAsset.GetOutputTrack(6), Player.GetPlayer().TopAnim);
    }

    private void OnTriggerEnter(Collider other) {
        sendTrigger.Invoke(this);
    }
    private void OnTriggerExit(Collider other) {
        sendTrigger.Invoke(null);
    }
    public void PlayTimeline() {
        enemy.TeleportPlayer();
        timeline.Play();
    }
}
