using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class CinematicManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera openingSceneCam;
    [SerializeField] private CinemachineVirtualCamera openingSceneCam2;
    [SerializeField] private CinemachineVirtualCamera main;
    public static event UnityAction unfade;
    private void Awake()
    {
        GameController.onNewGame += OpeningScene;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OpeningScene() {
        if (unfade != null) {
            unfade();
        }
        StartCoroutine(WaitToSwitchCams(openingSceneCam,openingSceneCam2));
        StartCoroutine(WaitToSwitchCams(openingSceneCam2, main));
    }
    private IEnumerator WaitToSwitchCams(CinemachineVirtualCamera cam1,CinemachineVirtualCamera cam2) {
        yield return new WaitUntil(() => cam1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 0.98f);
        cam1.Priority = 0;
       cam2.gameObject.SetActive(true);
        
    }
}
