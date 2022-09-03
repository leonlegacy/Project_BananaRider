using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VechleDurity : MonoBehaviour
{
    public int life = 100;

    public event Action LifeBecomeZero;

    private void Update()
    {
        if (life <= 0)
        {
            LifeBecomeZero?.Invoke();
        }
        else
        {
            life -= 1;
        }
    }
}
