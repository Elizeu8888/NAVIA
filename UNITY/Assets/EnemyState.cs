using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private float nextStateTimer;
    private string stateText;
    Animator anim;
    public float speed;
    private Quaternion _lookRotation;
    Rigidbody rb;

    enum States
    {
        Idle,
        Turn,
        Walk
    }

    States state;

    float x;
    float y;
    float z;
    public Vector3 pos;

    void Start()
    {
        state = States.Idle;
        nextStateTimer = 2;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        ProcessStates();

    }
    // State logic - switch states depending on what logic we want to apply
    void ProcessStates()
    {
        nextStateTimer -= Time.deltaTime;

        if (state == States.Idle)
        {
            Idle();

            if (nextStateTimer < 0)
            {
                state = States.Turn;
                nextStateTimer = 3;
            }
        }

        if (state == States.Turn)
        {

            Turn();
            if (nextStateTimer < 0)
            {
                state = States.Walk;
                nextStateTimer = 1;
            }

        }

        if (state == States.Walk)
        {
            if (nextStateTimer < 0)
            {
                state = States.Idle;
                nextStateTimer = 3;
            }

            Walk();
        }



    }

    void Idle()
    {
        stateText = "Idle";
        anim.SetBool("IsRunning", false);
        x = Random.Range(-1, 5f);
        z = Random.Range(-1f, 5f);
        y = 0f;
        pos = new Vector3(x, y, z);

    }

    void Turn()
    {
        stateText = "Turn";
        _lookRotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 4);
        Debug.Log(pos);

    }

    void Walk()
    {
        anim.SetBool("IsRunning", true);
        stateText = "Walk";
        //transform.position = Vector3.MoveTowards(transform.position, pos, speed* Time.deltaTime);
        rb.velocity = pos * speed * Time.deltaTime;
        Debug.Log(pos);
    }

}
