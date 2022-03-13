using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSCRIP : MonoBehaviour
{


    public GameObject fire,mainM,settingM,controlsM,audioM;
    public Color green,red;
    public Image fireimage;
    public Button start, audio, exit, audioBack;

    public float aTime = 1f;

    public void Play()
    {
        StartCoroutine(CoruPlay());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        StartCoroutine(CoruSetting());
    }

    public void Back()
    {
        StartCoroutine(CoruBack());
    }

    public void Cont()
    {
        StartCoroutine(CoruControls());
    }

    public void Audi()
    {
        StartCoroutine(CoruAudio());
    }


    IEnumerator CoruControls()
    {
        fire.SetActive(true);
        mainM.SetActive(false);
        controlsM.SetActive(true);
        exit.Select();
        yield return new WaitForSeconds(1);
        fire.SetActive(false);
    }
    IEnumerator CoruPlay()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator CoruSetting()
    {
        fire.SetActive(true);
        mainM.SetActive(false);
        settingM.SetActive(true);
        audio.Select();
        yield return new WaitForSeconds(1);
        fire.SetActive(false);
    }
    IEnumerator CoruBack()
    {
        fire.SetActive(true);
        mainM.SetActive(true);
        settingM.SetActive(false);
        controlsM.SetActive(false);
        audioM.SetActive(false);
        start.Select();
        yield return new WaitForSeconds(1);
        fire.SetActive(false);
    }

    IEnumerator CoruAudio()
    {
        fire.SetActive(true);
        mainM.SetActive(false);
        audioM.SetActive(true);
        settingM.SetActive(false);
        audioBack.Select();
        yield return new WaitForSeconds(1);
        fire.SetActive(false);

    }



}
