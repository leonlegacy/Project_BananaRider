using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3(0, player.position.y + 5.5f, player.position.z - 6.5f) + offset; 
    }
}
