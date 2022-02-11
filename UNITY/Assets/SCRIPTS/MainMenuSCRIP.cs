using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSCRIP : MonoBehaviour
{


    public GameObject fire,mainM,settingM,controlsM;
    public Color green,red;
    public Image fireimage;

    public float aTime = 1f;

    public void Play()
    {
        StartCoroutine(CoruPlay());
    }

    public void Exit()
    {

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



    IEnumerator CoruControls()
    {
        for (float t = 0f; t < 0.1f; t++)
            fireimage.color = new Vector4(1f - t, 1f, t, 1f);
        //fireimage.color = green;
        mainM.SetActive(false);
        controlsM.SetActive(true);
        yield return new WaitForSeconds(1);
        for (float t = 0f; t < 5f; t++)
            fireimage.color = new Vector4(t, 1f, t, 1f);
    }
    IEnumerator CoruPlay()
    {
        fireimage.color = red;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator CoruSetting()
    {

        for (float t = 0f; t < 0.1f; t++)
            fireimage.color = new Vector4(1f - t, 1f, t, 1f);
        //fireimage.color = green;
        mainM.SetActive(false);
        settingM.SetActive(true);
        yield return new WaitForSeconds(1);
        for (float t = 0f; t < 5f; t++)
            fireimage.color = new Vector4(t, 1f, t, 1f);

    }
    IEnumerator CoruBack()
    {

        for (float t = 0f; t < 0.1f; t++)
            fireimage.color = new Vector4(1f - t, 1f, t, 1f);
        mainM.SetActive(true);
        settingM.SetActive(false);
        controlsM.SetActive(false);
        yield return new WaitForSeconds(1);
        for (float t = 0f; t < 5f; t++)
            fireimage.color = new Vector4(t, 1f, t, 1f);
    }





}
