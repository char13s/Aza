using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AreaTransition : MonoBehaviour
{

    [SerializeField] private GameObject nextArea;
    private bool traveling;
    private Coroutine waitCoroutine;
    private Image black;
    [SerializeField] private int area;
    private GameObject forestProcessor;
    private GameObject graveyardProcessor;
    // Start is called before the first frame update
    void Start()
    {
        forestProcessor = GameController.GetGameController().ForestProcessor;
        graveyardProcessor = GameController.GetGameController().GraveyardProcessor;
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

            Player.GetPlayer().Nav.enabled = false;
            
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
        Debug.Log("fading");
        

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
        Debug.Log("ouchhhhh");
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
        Player.GetPlayer().Nav.enabled = true;
    }
}
