using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VechleDurity : MonoBehaviour
{
    public float life = 100;

    public event Action LifeBecomeZero;
    private bool playerRide = false;


    private void Update()
    {
        if (playerRide)
        {
            if (life <= 0)
            {
                LifeBecomeZero?.Invoke();
            }
            else
            {
                life -= Time.deltaTime;
            }
        }  
    }

}
