using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : CanvasManager
{
    [SerializeField] private GameObject episodeList;
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    public void EpisodeSelectControl(bool val) => episodeList.SetActive(val);
    private void Start() {
        LevelManager.turnOnMain += OnMainMenu;
    }
    private void OnMainMenu() {
        CanvasControl(true);
        menu.SetActive(true);
    }
}
