using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip swing;
    [SerializeField] private AudioClip bang;
    [SerializeField] private AudioClip rock;
    private AudioSource audio;
    private static AudioManager instance;

    public AudioClip Swing { get => swing; set => swing = value; }
    public AudioClip Bang { get => bang; set => bang = value; }
    public AudioClip Rock { get => rock; set => rock = value; }

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
        audio.Play();
    }
}
