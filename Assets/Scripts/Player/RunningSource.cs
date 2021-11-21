using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSource : MonoBehaviour
{
    private AudioSource sound;
    private float sfxVolume;

    public float SfxVolume { get => sfxVolume; set { sfxVolume = value;sound.volume = sfxVolume; } }

    // Start is called before the first frame update
    private void Awake() {
        sound = GetComponent<AudioSource>();
    }
    void Start()
    {
        Player.playSound += SoundControl;
        UiManager.sendSfxVolume += SetSfxVolume;
        SfxVolume = 0.3f;
    }
    private void SetSfxVolume(float val) {
        SfxVolume = val;
    }
    private void SoundControl(int val) {
        switch (val) {
            case 0:
               
                sound.Stop();
                break;
            case 1:

                if (!sound.isPlaying) {
                    sound.Play();
                }
                
                break;

        }
        //sound.Play();
    }
}
