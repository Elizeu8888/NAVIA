using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public UnityEvent dead;

    public void Dead()
    {
        dead.Invoke();
    }
}
