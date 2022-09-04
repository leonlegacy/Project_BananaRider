using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerControl : MonoBehaviour
{
    public event System.Action DropHole;
    public event System.Action Overtilt;

    #region Variable region
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
    Transform playerCharacter;


    float horizonForce = 0;
    float octupusForce = 0;
    bool canApply = false;
    bool canPunish = false;
    Rigidbody rigi;

    Collider coll;

    #endregion


    #region Awake and Start region
    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Start()
    {
        StartCoroutine(SetPunishable());
        
    }
    #endregion

    private void Update()
    {
        horizonForce -= GetToLeft();
        horizonForce += GetToRight();
        horizonForce += octupusForce;

        Vector3 direction = (transform.right * horizonForce * forceScale + transform.forward * forwardForce);
        rigi.velocity = new Vector3(direction.x * Time.deltaTime, rigi.velocity.y, direction.z);

        playerCharacter.rotation = Quaternion.Euler(transform.rotation.x,
                                                transform.rotation.y,
                                                Mathf.Clamp(horizonForce / 2f, -80f, 80f));

        if (horizonForce / 2f < -80f || horizonForce / 2f > 80f)
            Overtilt?.Invoke();

        GetMouseStatus();
        MousePunishment();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Hole":
                DropHole?.Invoke();
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Vechle":
                if(canApply)
                    Ride(other.transform.parent.GetComponent<VehicleDurity>());
                break;
        }
    }

    float GetToLeft()   //Get the force to Left
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

    float GetToRight()  //Get the force to Right
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

    IEnumerator SetPunishable() //Set mouse punishment to true in 1 second
    {
        yield return new WaitForSeconds(1);

        canPunish = true;
    }
    IEnumerator SetOctupusPunishable()
    {
        yield return new WaitForSeconds(3);
        octupusForce = 0;
    }

    public void SetOctupusForce(float _force)   //Call to set Octupus force and start punishment
    {
        octupusForce = _force;
        StartCoroutine(SetOctupusPunishable());
    }


    public void Disable()
    {
        coll.enabled = false;
        rigi.isKinematic = true;
        enabled = false;
    }

    public void Enable()
    {
        coll.enabled = true;
        rigi.isKinematic = false;
        enabled = true;
    }

    public float GetHorizontalForce()   //Get the total force for horizontal movement
    {
        return horizonForce * forceScale;
    }

    public float GetForwardForce()  //Get the forward force 
    {
        return forwardForce;
    }
}