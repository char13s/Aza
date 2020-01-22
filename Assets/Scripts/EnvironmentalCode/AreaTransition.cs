using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
public class AreaTransition : MonoBehaviour
{

    [SerializeField] private GameObject nextArea;
    private bool traveling;
    private Coroutine waitCoroutine;
    private Image black;
    [SerializeField] private int area;
    private GameObject forestProcessor;
    private GameObject graveyardProcessor;

    public static UnityAction rock;
    public static event UnityAction transition;
    public static event UnityAction movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        forestProcessor = PostProcessorManager.GetProcessorManager().ForestProcessor;
        graveyardProcessor = PostProcessorManager.GetProcessorManager().GraveyardProcessor;
        black = UiManager.GetUiManager().Black;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (movePlayer != null) {
                movePlayer();
            }
            
            StartCoroutine(FadeOutCoroutine());
            StartCoroutine(FadeCoroutine());

        }
    }
    private IEnumerator FadeOutCoroutine()
    {
        while (isActiveAndEnabled&&black.color.a <= 0.99)
        {
            yield return null;      
                Fade();
        }
    }
    private void Fade()
    {

        

            Color color = black.color;
            color.a += 0.03f;
            black.color = color;
    }
    private void AreaSwitches() {
        ProcessorTurnOff();
        switch (area) {

            case 1:
                forestProcessor.SetActive(true);
                break;
            case 2:
                if (rock != null) {

                    rock();
                }
                graveyardProcessor.SetActive(true);
                break;
        }

    }
    private void ProcessorTurnOff() {
        forestProcessor.SetActive(false);
        graveyardProcessor.SetActive(false);
    }
    private void NextScene()
    {StopCoroutine(FadeOutCoroutine());

        Player.GetPlayer().transform.position = nextArea.transform.position;
        Color color = black.color;
        color.a = 0;
        black.color = color;
    }
    private IEnumerator FadeCoroutine()
    {

        yield return new WaitUntil(() => black.color.a >= 0.98);
        StartCoroutine(WaitCoroutine());
    }
    private IEnumerator WaitCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(1);
        
        yield return wait;
        AreaSwitches();
        NextScene();
        if (transition != null) {
            transition();
        }
        Player.GetPlayer().Nav.enabled = true;
        
        Player.GetPlayer().InputSealed = false;
    }
}
