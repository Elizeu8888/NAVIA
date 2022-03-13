using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CamAnim : MonoBehaviour
{

    public Button play;
    public Animator anim,startAnim;
    public GameObject startmenu, mainmenu;

    void Update()
    {
        if(Input.anyKey)
        {
            anim.SetBool("STARTED", true);
            startAnim.SetTrigger("pressed");
            //startmenu.SetActive(false);
        }
    }


    public void Menu()
    {
        mainmenu.SetActive(true);
        play.Select();
    }



}
