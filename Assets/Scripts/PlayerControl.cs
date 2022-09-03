using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public partial class PlayerControl : MonoBehaviour
{
    [Header("Speed Level value")]
    public float lv1 = 1f;
    public float lv2 = 2f;
    public float lv3 = 3f;

    [SerializeField]
    public float forwardForce = 5f;

    [Header(" ")]
    [SerializeField]
    float forceScale = 10f;

    [SerializeField]
    float mousePenalty = 100f;

    [SerializeField]
    Text debugText;


    float horizonForce = 0;
    bool canApply = false;
    bool canPunish = false;
    Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(SetPunishable());
    }

    private void Update()
    {
        horizonForce -= GetToLeft();
        horizonForce += GetToRight();

        Vector3 direction = (transform.right * horizonForce * forceScale + transform.forward * forwardForce);
        rigi.velocity = new Vector3(direction.x * Time.deltaTime, rigi.velocity.y, direction.z);

        GetMouseStatus();
        ShowDebugger();
        Debug.Log(rigi.velocity);
    }

    private void FixedUpdate()
    {
        //float force = horizonForce * forceScale;
        //rigi.AddForce(Vector3.right * force);
    }

    float GetToLeft()
    {
        float _force = 0;
        if (Input.GetKey(KeyCode.A))
            _force += lv3;
        else if (Input.GetKey(KeyCode.S))
            _force += lv2;
        else if (Input.GetKey(KeyCode.D))
            _force += lv1;

        return _force;
    }

    float GetToRight()
    {
        float _force = 0;
        if (Input.GetKey(KeyCode.G))
            _force += lv1;
        else if (Input.GetKey(KeyCode.H))
            _force += lv2;
        else if (Input.GetKey(KeyCode.J))
            _force += lv3;

        return _force;
    }

    void GetMouseStatus()   //Check if the mouse has clicked
    {
        if (Input.GetButton("Fire1")) canApply = true;
        if (Input.GetButtonUp("Fire1"))
        {
            canApply = false;
        }
    }

    void MousePunishment()  //Punishment for moving the mouse
    {
        float mx = Input.GetAxis("Mouse X");
        if(canPunish)
            rigi.AddForce(Vector3.right * mx * mousePenalty * Random.Range(0.5f, 20f));
    }

    void ShowDebugger() //Debug UI function
    {
        debugText.text = //"L = " + toLeft.ToString() + "\n" +
                        //"R = " + toRight.ToString() + "\n" +
                        "Force = " + (horizonForce.ToString()) + "\n" +
                        "canApply = " + canApply;
    }

    IEnumerator SetPunishable() //Not in use
    {
        yield return new WaitForSeconds(1);

        canPunish = true;
    }

    public float GetHorizontalForce()
    {
        return horizonForce * forceScale;
    }

    public float GetForwardForce()
    {
        return forwardForce;
    }
}