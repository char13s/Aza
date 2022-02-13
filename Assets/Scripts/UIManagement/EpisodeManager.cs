using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EpisodeManager : MonoBehaviour
{
    //public static event UnityAction<int> setContinue;
    private int lastEpisode;

    public int LastEpisode { get => lastEpisode; set => lastEpisode = value; }

    // Start is called before the first frame update
    void Start() {

    }
    private void Continue() {
        
    }
    public void SetLastEpisode(int val) => LastEpisode = val;
}