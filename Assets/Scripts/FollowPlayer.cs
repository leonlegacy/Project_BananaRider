using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float y = 15;
    public float z = 25;


    // Update is called once per frame
 
    void LateUpdate()
    {
        
        float h = Input.GetAxis("Horizontal");
        
        //Follow player
        transform.position = player.position + offset ;

        //Rotate
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, -h * Time.deltaTime * y, h * Time.deltaTime * z);

        

    }




}
