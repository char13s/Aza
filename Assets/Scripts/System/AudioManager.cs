using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private GameObject sfxSource;
    [Space]
    [SerializeField] private AudioClip swing;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip bang;
    [SerializeField] private AudioClip rock;
    [SerializeField] private AudioClip slimeHit;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip hitShield;
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip buttonClick;
    

    [Header("Zend Sounds")]
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip drawSword;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip doubleJump;
    [Header("SoundTracks")]
    [SerializeField] private AudioClip soundTracks;
    [SerializeField] private AudioClip titleScreen;
    [SerializeField] private AudioClip houseMusic;
    [SerializeField] private AudioClip level1;
    [SerializeField] private AudioClip wormDiving;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip mysteriousHarmonies;

    
    private AudioSource masterAudio;
    private AudioSource sfxAudio;
    private static AudioManager instance;

    private float masterVolume;
	private float sfxVolume;

    public AudioClip Swing { get => swing; set => swing = value; }
    public AudioClip Bang { get => bang; set => bang = value; }
    public AudioClip Rock { get => rock; set => rock = value; }
    public AudioClip SlimeHit { get => slimeHit; set => slimeHit = value; }
    public AudioClip LandingSound { get => landingSound; set => landingSound = value; }
    public AudioClip Jump { get => jump; set => jump = value; }
    public AudioClip DrawSword { get => drawSword; set => drawSword = value; }
    public AudioClip Dash { get => dash; set => dash = value; }
    public AudioClip DoubleJump { get => doubleJump; set => doubleJump = value; }
    public AudioClip Hit { get => hit; set => hit = value; }
    public AudioClip HitShield { get => hitShield; set => hitShield = value; }
    public float MasterVolume { get => masterVolume; set { masterVolume = value;masterAudio.volume = masterVolume; } }
    public float SfxVolume { get => sfxVolume; set { sfxVolume = value;sfxAudio.volume = sfxVolume; } }

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
        masterAudio = GetComponent<AudioSource>();
        sfxAudio = sfxSource.GetComponent<AudioSource>();
        AreaTransition.rock += SetMusic;
        GameController.titleScreen += Fade;
        //GameController.onoLevelLoaded+=
        FreeFallZend.diving += Fade;
        
    }
    // Start is called before the first frame update
    void Start()
    {
		UiManager.sendSfxVolume += SetSfxVolume;
        UiManager.sendMasterVolume += SetMasterVolume;
        UiManager.pause += Pause;
        Player.sendSfx += RetrieveSfx;
        CommandInputBehavior.sendsfx += RetrieveSfx;
        Enemy.sendsfx += RetrieveSfx;
        UIButton.click += OnClick;
        DialogueManager.requestNextLine += OnClick;
        Inventory.set += OnClick;
        MasterVolume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void SetMasterVolume(float val) {
		MasterVolume = val;
	}
	private void SetSfxVolume(float val) {
		SfxVolume = val;
	}
	private void SetMusic()
    {
        masterAudio.clip = Rock;
        
    }
    private void FadeOutVolume() {
        masterAudio.volume-=0.1f;
        
    }
     
    private IEnumerator FadeOutCoroutine(int val) {
        YieldInstruction wait = new WaitForSeconds(0.3f);
        while (isActiveAndEnabled && masterAudio.volume > 0f) {
            yield return null;
            FadeOutVolume();
        }
        StartCoroutine(FadeOut(val));
    }
    private IEnumerator FadeOut(int val) {
        yield return new WaitUntil(()=> masterAudio.volume == 0);
        BackGroundMusicManager(val);

    }
    private void Fade(int val) {
        StartCoroutine(FadeOutCoroutine(val));
        //BackGroundMusicManager(val);
    }
    private void BackGroundMusicManager(int trackNumber) {
        
        switch (trackNumber) {
            case 0:
                masterAudio.clip = null;
                break;
            case 1:
                masterAudio.clip = titleScreen;
                break;
            case 2:
                masterAudio.clip = death;
                break;
            default:
                masterAudio.clip = wormDiving;
                break;
        }
        masterAudio.volume = MasterVolume;
        masterAudio.Play();
    }
    private void RetrieveSfx(AudioClip sound) {
        sfxAudio.PlayOneShot(sound);
    }
    public void OnClick() {
        sfxAudio.PlayOneShot(buttonClick);
    }
    public void OnSelected() {
        sfxAudio.PlayOneShot(click);
    }
    private void Pause() {
        sfxAudio.PlayOneShot(pause);
    }

}
