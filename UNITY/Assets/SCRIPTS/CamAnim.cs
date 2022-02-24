using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnim : MonoBehaviour
{


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
    }



}
