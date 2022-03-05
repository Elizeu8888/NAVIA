 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool attacking;
    public float health = 100;
    public float xp = 0;

    public float jumpheight;
    Vector3 direction;

    public Rigidbody rb;
    public Transform cam,camAIM;
    public float speed = 6f;
    public float normalspeed, sprintspeed, atkMoveSpeed,dashSpeed, jumpVelocity;


    public float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 1f;
    float mouseXSmooth;
    Vector3 rbVelocity;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isgrounded,isdashing;

    bool dashcooldown = false;
    public float timeRemaining = 0.5f;

    public Animator anim;
    public GameObject weaponMenu;
    public ParticleSystem particleSystem;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(PlayerDeath), 0.5f);
    }
    private void PlayerDeath()
    {
        print("dead");
    }

    void Start()
    {

        //Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        //Cursor.lockState = CursorLockMode.None;     // set to default default
    }


    public void StopDashing()
    {       
        anim.SetLayerWeight(3, 0);
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (anim.GetCurrentAnimatorStateInfo(2).IsTag("attack"))
        {
            attacking = true;
            anim.SetLayerWeight(2, 1);
            var lookDir = transform.position - camAIM.position;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
            anim.SetBool("attacking", true);
        }
        else
        {
            anim.SetBool("attacking", false);
            attacking = false;
            anim.SetLayerWeight(2, 0);
        }



        if(Input.GetKeyDown("q") && dashcooldown == false)
        {
            isdashing = true;
            anim.SetBool("dashing", true);
        }

        if (isdashing == true)
        {

            for (float a = 0.1f; a>0; a -= 0.1f * Time.deltaTime)
            {
                particleSystem.Play();
                float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

                Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward * Time.deltaTime;// here is the movement
                rb.AddForce(movedir.normalized * dashSpeed * Time.deltaTime, ForceMode.VelocityChange);
                anim.Play("dash");
                anim.SetLayerWeight(3, 1);
            }
            isdashing = false;
            dashcooldown = true;
            anim.SetBool("dashing", false);
        }
        else
        {
            particleSystem.Stop();
        }

        if(dashcooldown == true)
        {
            timeRemaining -= Time.deltaTime;
            
        }

        if (timeRemaining < 0)
        {
            dashcooldown = false;
            timeRemaining = 1f;
            print("dashed");
        }




        if (attacking == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sprinting();
                anim.SetBool("Sprinting", true);
            }
            else
            {
                Movement();
                anim.SetBool("Sprinting", false);
            }
        }
        else
        {
            AtkMove();
        }



        anim.SetBool("walking", false);
        if ((rb.velocity.magnitude > 1) && isgrounded)
        {
            anim.SetBool("walking", true);

        }
        else
        {
            anim.SetBool("walking", false);
        }
        if(isgrounded == false)
        {
            anim.SetBool("Grounded", false);
        }
        else
        {
            anim.SetBool("Grounded", true);
        }


        anim.SetFloat("Z", direction.z);
        anim.SetFloat("X", mouseXSmooth + direction.x);
        mouseXSmooth = Mathf.Lerp(mouseXSmooth, Input.GetAxis("Mouse X"), 4 * Time.deltaTime);

        if (rb.velocity.y > 0)
        {
            anim.SetBool("rising", true);
            
        }
        else
        {
            
            anim.SetBool("rising", false);
        }
    }


    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        Jump();
        if(rb.velocity.y > 0)
        {
            if (rb.velocity.sqrMagnitude > jumpVelocity)
            {
                Vector3 endVelocity = rb.velocity;
                endVelocity.y *= 0.9f;
                rb.velocity = endVelocity;

            }
        }

    }

    void Movement()
    {
        if (direction.magnitude >= 0.1f && !weaponMenu.activeSelf)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement





            if (attacking == false)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            





            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            rb.AddForce(movedir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            if (isgrounded == true)
            {
                Vector3 resultVelocity = rb.velocity;
                resultVelocity.z = 0;
                resultVelocity.x = 0;
                rb.velocity = resultVelocity;
            }


        }
        if (rb.velocity.sqrMagnitude > maxVelocity)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;

        }
    }
    void AtkMove()
    {
        if (direction.magnitude >= 0.1f && !weaponMenu.activeSelf)
        {

            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement

            if (attacking == false)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            rb.AddForce(movedir.normalized * atkMoveSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            if (isgrounded == true)
            {
                Vector3 resultVelocity = rb.velocity;
                resultVelocity.z = 0;
                resultVelocity.x = 0;
                rb.velocity = resultVelocity;
            }

        }
        if (rb.velocity.sqrMagnitude > maxVelocity)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;

        }
    }
    void Jump()
    {
        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up.normalized * jumpheight, ForceMode.Impulse);// here u jump
            isgrounded = false;
        }
    }

    void Sprinting()
    {
        if (direction.magnitude >= 0.1f && !weaponMenu.activeSelf)
        {

            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement


            if (attacking == false)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }


            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;// here is the movement
            rb.AddForce(movedir.normalized * sprintspeed * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            if (isgrounded == true)
            {
                Vector3 resultVelocity = rb.velocity;
                resultVelocity.z = 0;
                resultVelocity.x = 0;
                rb.velocity = resultVelocity;
            }

        }
        if (rb.velocity.sqrMagnitude > sprintspeed)// right alt and shift for||||
        {
            Vector3 endVelocity = rb.velocity;
            endVelocity.z *= 0.9f;
            endVelocity.x *= 0.9f;
            rb.velocity = endVelocity;

        }
    }
}
