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

    [Header(" ")]
    [SerializeField]
    float forceScale = 10f;

    [SerializeField]
    Text debugText;

    float toRight = 0, toLeft = 0;
    bool canApply = false;
    Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        toLeft += GetToLeft();
        toRight += GetToRight();
        GetMouseStatus();

        debugText.text = "L = " + toLeft.ToString() + "\n" +
                        "R = " + toRight.ToString() + "\n" +
                        "Force = " + (toRight - toLeft) + "\n" +
                        "canApply = " + canApply;

        /*
        Debug.Log("L = " + toLeft);
        Debug.Log("R = " + toRight);
        Debug.Log("Force = " + (toRight - toLeft));
        Debug.Log("canApply = " + canApply);
        */
    }

    private void FixedUpdate()
    {
        float force = (toRight - toLeft) * forceScale;
        rigi.AddForce(Vector3.right * force);

    }

    float GetToLeft()
    {
        float _force = 0;
        if (Input.GetKeyDown(KeyCode.A))
            _force += lv3;
        else if (Input.GetKeyDown(KeyCode.S))
            _force += lv2;
        else if (Input.GetKeyDown(KeyCode.D))
            _force += lv1;

        return _force;
    }

    float GetToRight()
    {
        float _force = 0;
        if (Input.GetKeyDown(KeyCode.G))
            _force += lv1;
        else if (Input.GetKeyDown(KeyCode.H))
            _force += lv2;
        else if (Input.GetKeyDown(KeyCode.J))
            _force += lv3;

        return _force;
    }

    void GetMouseStatus()
    {
        if (Input.GetButton("Fire1")) canApply = true;
        if (Input.GetButtonUp("Fire1"))
        {
            canApply = false;
            toLeft = 0;
            toRight = 0;
        }
    }

}
