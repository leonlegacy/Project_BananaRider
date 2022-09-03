using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VechleDurity : MonoBehaviour
{

    public event Action lifeBecomeZero;
    public event Action<float> changeLife;

    public float maxLife = 100;
    public float life { get; private set; }
    public bool playerRide { get; private set; }


    private void Start()
    {
        life = maxLife;
    }

    private void Update()
    {
        if (playerRide)
        {
            if (life <= 0)
            {
                lifeBecomeZero?.Invoke();
            }
            else
            {
                life -= Time.deltaTime;
                changeLife?.Invoke(life);
            }
        }  
    }

    private void ChangePlayerRide(bool value)
    {
        playerRide = value;

        if ( playerRide == false)
        {
            Destroy(gameObject);
        }
    }
}
