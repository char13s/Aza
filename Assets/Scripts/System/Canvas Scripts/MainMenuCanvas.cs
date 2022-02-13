using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : CanvasManager
{
    [SerializeField] private GameObject episodeList;
    // Start is called before the first frame update
    public void EpisodeSelectControl(bool val) => episodeList.SetActive(val);
}
