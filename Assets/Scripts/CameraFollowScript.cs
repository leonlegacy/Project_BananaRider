using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    Vector3 tpsOffset;

    [SerializeField]
    float smoothSpeed = 0.125f;

    [SerializeField]
    float y = 15f;

    [SerializeField]
    float z = 25f;

    [SerializeField]
    bool isThirdPerson = false;

    Vector3 desiredPosition;

    private void Awake()
    {
        //
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) isThirdPerson = !isThirdPerson;

        if (isThirdPerson)
        {
            float h = Input.GetAxis("H");

            desiredPosition = player.position + tpsOffset;
            transform.localRotation = transform.localRotation * Quaternion.Euler(0, h * Time.deltaTime * y, h * Time.deltaTime * z);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(26f, 0f, 0f);
            desiredPosition = new Vector3(0, player.position.y, player.position.z) + offset;
        }

    }

    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetThirdPersonCamera(bool _state)
    {
        isThirdPerson = _state;
    }
}
