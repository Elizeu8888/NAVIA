using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sldr;
    public AudioMixer mixerSFX;
    public Slider sldrSFX;
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sldr.value) * 20);
        
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixerSFX.SetFloat("SFXvol", Mathf.Log10(sldrSFX.value) * 20);
        print("gg");
    }


    /*void start()
    {
        if (PlayerPrefs.HasKey("volume") == true)
        {
            sldr.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            sldr.value = 0.001f;
        }
    }*/
}
