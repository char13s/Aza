using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LoadingCanvas :CanvasManager
{
    public static event UnityAction changeScene;
    [SerializeField] private Image black;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeTime;
    private void Awake() {
         canvas.SetActive(true);
    }
    private void Start() {
       
        //LevelManager.newScene += LevelChange;
        LevelManager.levelFinished += UnFade;
        LevelManager.off += LevelChange;
    }
    private void ClearScreen() {
        Color color = black.color;
        color.a = 0;
        black.color = color;
    }
    public void LevelChange() {

        StartCoroutine(FadeOutCoroutine());
    }
    private IEnumerator FadeOutCoroutine() {
        while (isActiveAndEnabled && black.color.a <= 0.99) {
            yield return null;
            FadeToBlack();
        }
        if (black.color.a >= 0.99) {
            if (changeScene != null) {
                changeScene();
            }
        }
    }
    private void FadeToBlack() {
        Color color = black.color;
        color.a += fadeSpeed;
        black.color = color;

    }
    private void UnFade(bool val) {
        StartCoroutine(WaitToUnFade());
        print("Unfade requested");
    }
    private IEnumerator WaitToUnFade() {
        YieldInstruction wait = new WaitForSeconds(fadeTime);
        yield return wait;
        StartCoroutine(FadeBackIn());
    }
    private IEnumerator FadeBackIn() {
        while (isActiveAndEnabled && black.color.a >= 0) {
            yield return null;
            print("FADING");
            Color color = black.color;
            color.a -= fadeSpeed;
            black.color = color;
        }
    }

}
