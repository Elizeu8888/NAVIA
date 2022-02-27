using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Audiomanager : MonoBehaviour
{
    public Sound[] sounds;
    public static Audiomanager instance = null;
    public AudioMixerGroup audMix;


    float sfxVol, musicVol;

    public AudioMixer mixer;
    public Slider sldr;
    public AudioMixer mixerSFX;
    public Slider sldrSFX;
    public Toggle musicONOFF;
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sldr.value) * 20);
        PlayerPrefs.SetFloat("MusicVol", sldr.value);
    }

    public void SetLevelSFX(float sliderValue)
    {
        mixerSFX.SetFloat("SFXvol", Mathf.Log10(sldrSFX.value) * 20);
        PlayerPrefs.SetFloat("SFXvol", sldrSFX.value);
    }

    void Start()
    {
        sfxVol = PlayerPrefs.GetFloat("SFXvol",1);
        musicVol = PlayerPrefs.GetFloat("MusicVol",1);

        mixerSFX.SetFloat("SFXvol", sfxVol);
        mixer.SetFloat("MasterVol", musicVol);
        sldrSFX.value = sfxVol;
        sldr.value = musicVol;
        Play("Theme");

        if(PlayerPrefs.GetInt("MusicOn") == 1)
        {
            musicONOFF.isOn = true;
        }
        else
        {
            musicONOFF.isOn = false;
        }


    }


    public void Toggle(bool music)
    {
        if(music == false)
        {
            sldr.enabled = false;
            sldr.value = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else
        {
            sldr.enabled = true;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
    }




    void Awake()
    {



        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            
        }
        DontDestroyOnLoad(gameObject);



        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.audioMixer;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }



    }


    public void Buttonsound()
    {
        Play("click");

    }



    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found! ");
            return;
        }
            
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found! ");
            return;
        }

    }

}
