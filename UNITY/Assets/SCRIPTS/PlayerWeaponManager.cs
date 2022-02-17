using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{

    public Collider[] weaponsCOLLIDER;
    public GameObject[] weapons;
    GameObject weapon;
    public Transform rightHand;

    public bool weapondrawn = false;
    public Animator anim;

    public int weaponNumber;
    public GameObject weaponMenu;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            if (weapondrawn == false)
            {
                weaponMenu.SetActive(true);
            }
            else if (weaponMenu.activeSelf)
            {
                weaponMenu.SetActive(false);
            }
            weapondrawn = !weapondrawn;
            
        }
           

        if(weaponMenu.activeSelf)           
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;

        if (weapondrawn == false)
        {
            anim.SetBool("weaponOUT", false);
        }


    }
    public void LaunchAttack(Collider col, float damage)
    {

        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitBoxes"));
        foreach (Collider c in cols)
        {


            if (c.transform.parent == transform)
            {
                continue;
            }

            Debug.Log(c.tag);

            switch (c.tag)
            {
                case "enemy":
                    
                    break;
                default:
                    Debug.Log("nopedidntwork");
                    break;

            }
            float timer = 1;

            c.SendMessageUpwards("TakeDamage", damage);

        }

    }
    public void WeaponActivate(GameObject wep)
    {
        weapon = Instantiate(wep, new Vector3(0,0,0), Quaternion.identity);
        weapon.transform.parent = rightHand;
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;
    }
    public void RunitWeapon()
    {
        WeaponActivate(weapons[weaponNumber]);
    }
        
    public void RunicRedraw()
    {
        Destroy(weapon);
    }
        
    public void LayerWeight()
    {
        anim.SetLayerWeight(1, 0);
    }


    public void Weapon1()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 0;
        weaponMenu.SetActive(false);
    }
    public void Weapon2()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 1;
        weaponMenu.SetActive(false);
    }
    public void Weapon3()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 2;
        weaponMenu.SetActive(false);
    }
    public void Weapon4()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetBool("weaponOUT", true);
        weaponNumber = 3;
        weaponMenu.SetActive(false);
    }
}