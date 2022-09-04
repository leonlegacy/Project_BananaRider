using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    #region Variable region
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

    public bool isSmallView = false;
    public bool isWaterView = false;
    public Material smallViewMat;
    public Material waterViewMat;

    Vector3 desiredPosition;

    #endregion

    private void Awake()
    {
        //Subscribe Camera VFX here (Total 3)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) isThirdPerson = !isThirdPerson;
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetSmallViewFX();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWaterViewFX();
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetThirdPersonFX();

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

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (isSmallView)
        {
            Graphics.Blit(src, dst, smallViewMat);
        }
        else if (isWaterView)
        {
            Graphics.Blit(src, dst, waterViewMat);
        }
        else
        {
            Graphics.Blit(src, dst);
        }
    }


    public void SetThirdPersonFX()    //For switching to ThirdPersonView
    {
        SetThirdPersonCamera(true);
        isSmallView = false;
        isWaterView = false;
    }

    public void SetSmallViewFX()  //For switching to SmallView
    {
        SetThirdPersonCamera(false);
        isSmallView = true;
        isWaterView = false;
    }

    public void SetWaterViewFX()  //For switching to WaterView
    {
        SetThirdPersonCamera(false);
        isSmallView = false;
        isWaterView = true;
    }

    public void SetNoneVFX()   //For turning off all VFX
    {
        SetThirdPersonCamera(false);
        isSmallView = false;
        isWaterView = false;
    }

    public void SetThirdPersonCamera(bool _state)   //Set ThirdPerson mode api
    {
        isThirdPerson = _state;
    }

    private void OnDestroy()
    {
        //Unsubscribe Camera VFX here (Total 3)
    }
}