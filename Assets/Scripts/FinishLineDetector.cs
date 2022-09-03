using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineDetector : MonoBehaviour
{
    public event Action hitFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hitFinishLine?.Invoke();
        }
    }
}
