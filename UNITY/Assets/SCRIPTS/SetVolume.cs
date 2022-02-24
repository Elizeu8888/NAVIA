using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sldr;
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MyExposedParam", Mathf.Log10(sldr.value) * 20);
        
    }
}
