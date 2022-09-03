using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Speed Level value")]
    public float lv1 = 1f;
    public float lv2 = 2f;
    public float lv3 = 3f;

    [SerializeField]
    float speedVelocity = 5f;

    [Header(" ")]
    [SerializeField]
    float forceScale = 10f;

    [SerializeField]
    Text debugText;

    [SerializeField]
    float scanTimer = 2f;

    float horizonForce = 0;
    bool canApply = false;
    bool canMove = true;
    Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //StartCoroutine(ScanKeyTimer());
    }

    private void Update()
    {
        horizonForce -= GetToLeft() * Time.deltaTime;
        horizonForce += GetToRight() * Time.deltaTime;

        GetMouseStatus();

        ShowDebugger();
        rigi.velocity = new Vector3(0, -5, speedVelocity);
        
        /*
        Debug.Log("L = " + toLeft);
        Debug.Log("R = " + toRight);
        Debug.Log("Force = " + (toRight - toLeft));
        Debug.Log("canApply = " + canApply);
        */
    }

    private void FixedUpdate()
    {
        float force = horizonForce * forceScale;
        rigi.AddForce(Vector3.right * force);

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

    void GetMouseStatus()
    {
        if (Input.GetButton("Fire1")) canApply = true;
        if (Input.GetButtonUp("Fire1"))
        {
            canApply = false;
        }
    }

    void ShowDebugger()
    {
        debugText.text = //"L = " + toLeft.ToString() + "\n" +
                        //"R = " + toRight.ToString() + "\n" +
                        "Force = " + (horizonForce.ToString()) + "\n" +
                        "canApply = " + canApply;
    }

    IEnumerator ScanKeyTimer()
    {
        yield return new WaitForSeconds(scanTimer);

        horizonForce -= GetToLeft();
        horizonForce += GetToRight();
        StartCoroutine(ScanKeyTimer());
    }
}
