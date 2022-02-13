using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float jumpheight;
    Vector3 direction;

    public Rigidbody rb;
    public Transform cam;
    public float speed = 6f;
    public float turnsmoothing = 0.1f;
    float turnsmoothvelocity = 0.5f;
    public float maxVelocity = 1f;
    float mouseXSmooth;
    Vector3 rbVelocity;

    public Transform groundcheck;
    public float grounddistance = 0.4f;
    public LayerMask groundmask;
    public bool isgrounded;

    public Animator anim;

    void Start()
    {

        //Cursor.lockState = CursorLockMode.Confined; // keep confined in the game window
        Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        //Cursor.lockState = CursorLockMode.None;     // set to default default
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");// uses imput to find direction
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        Movement();


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


    }


    void FixedUpdate()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);
        Jump();
    }

    void Movement()
    {
        if (direction.magnitude >= 0.1f)
        {


            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;// finds direction of movement





            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnsmoothing);// makes it so the player faces its movement direction
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            





            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward * Time.deltaTime;// here is the movement
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

    void Jump()
    {

        if (Input.GetKey("space") && isgrounded)
        {
            rb.AddForce(transform.up.normalized * jumpheight, ForceMode.Impulse);// here u jump
            isgrounded = false;
        }
    }
}
