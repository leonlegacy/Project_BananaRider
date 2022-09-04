using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObstacle : MonoBehaviour
{
    public event Action touchObstacle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchObstacle?.Invoke();
            GetComponent<AudioSource>().Play();
        }
    }

}
