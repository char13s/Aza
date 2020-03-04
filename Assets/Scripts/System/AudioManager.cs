using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip swing;
    [SerializeField] private AudioClip bang;
    [SerializeField] private AudioClip rock;
    [SerializeField] private AudioClip slimeHit;
    [SerializeField] private AudioClip titleScreen;
    [SerializeField] private AudioClip wormDiving;

    private AudioSource audio;
    private static AudioManager instance;

    public AudioClip Swing { get => swing; set => swing = value; }
    public AudioClip Bang { get => bang; set => bang = value; }
    public AudioClip Rock { get => rock; set => rock = value; }
    public AudioClip SlimeHit { get => slimeHit; set => slimeHit = value; }

    public static AudioManager GetAudio() => instance.GetComponent<AudioManager>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
        AreaTransition.rock += SetMusic;
        GameController.titleScreen += Fade;
        FreeFallZend.diving += Fade;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetMusic()
    {
        audio.clip =Rock;
        
    }
    private void FadeOutVolume() {
        audio.volume-=0.1f;
        
    }
     
    private IEnumerator FadeOutCoroutine(int val) {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        while (isActiveAndEnabled && audio.volume > 0f) {
            yield return null;
            Debug.Log("bro");

            FadeOutVolume();
        }
        StartCoroutine(FadeOut(val));
    }
    private IEnumerator FadeOut(int val) {
        yield return new WaitUntil(()=> audio.volume == 0);
        BackGroundMusicManager(val);

    }
    private void Fade(int val) {
        StartCoroutine(FadeOutCoroutine(val));
        //BackGroundMusicManager(val);
    }
    private void BackGroundMusicManager(int trackNumber) {  
        switch (trackNumber) {
            case 0:
                audio.clip = titleScreen;
                break;
            case 1:
                audio.clip = wormDiving;
                break;
        }
        audio.volume = 0.5f;
        audio.Play();
    }
}
