using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventSwitch : MonoBehaviour
{
    public UnityEvent use;
    public UnityEvent altUse;
    public UnityEvent altUse2;

    //public SoundManager soundscript;

    public void Use()
    {
        use.Invoke();
    }

    public void AltUse()
    {
        altUse.Invoke();
    }

    public void AltUse2()
    {
        altUse2.Invoke();
    }
}
