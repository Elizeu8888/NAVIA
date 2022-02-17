using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{

    public Collider[] weaponsCOLLIDER;
    public GameObject[] weapons;
    public Transform rightHand;

    public bool weapondrawn = false;
    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown("f"))
            weapondrawn = !weapondrawn;

        if(weapondrawn == true)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetBool("weaponOUT", true);
        }
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
        GameObject weapon = Instantiate(wep, new Vector3(0,0,0), Quaternion.identity);
        weapon.transform.parent = rightHand;
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;

    }


    public void RunitWeapon()
    {
        WeaponActivate(weapons[0]);
    }




}
