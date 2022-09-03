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

    [SerializeField]
    float scanTimer = 2f;

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

        GetMouseStatus();
        ShowDebugger();
        rigi.AddForce(Vector3.forward * forwardForce);
    }

    private void FixedUpdate()
    {
        float force = horizonForce * forceScale;
        rigi.AddForce(Vector3.right * force);
        MousePunishment();

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

        return _force * Time.deltaTime;
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

        return _force * Time.deltaTime;
    }

    void GetMouseStatus()
    {
        if (Input.GetButton("Fire1")) canApply = true;
        if (Input.GetButtonUp("Fire1"))
        {
            canApply = false;
        }
    }

    void MousePunishment()
    {
        float mx = Input.GetAxis("Mouse X");
        if(canPunish)
            rigi.AddForce(Vector3.right * mx * mousePenalty * Random.Range(0.5f, 20f));
    }

    void ShowDebugger()
    {
        debugText.text = //"L = " + toLeft.ToString() + "\n" +
                        //"R = " + toRight.ToString() + "\n" +
                        "Force = " + (horizonForce.ToString()) + "\n" +
                        "canApply = " + canApply;
    }

    IEnumerator SetPunishable()
    {
        yield return new WaitForSeconds(1);

        canPunish = true;
    }

    public float GetHorizontalForce()
    {
        return horizonForce * forceScale;
    }
}