using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDurity : MonoBehaviour
{
    public event Action lifeBecomeZero;
    public event Action<float> changeLife;
    public event Action<VehicleDurity> BeRideEvent;
    public event Action<VehicleDurity> DisrideEvent;

    public Sprite icon;
    public float maxLife = 100;
    [Header("前進倍率")]
    public float onwardRate = 1;
    [Header("左右倍率")]
    public float horizonRate = 1;

    [SerializeField]
    AudioSource VehicleSlidingSFX;

    public float life { get; private set; }
    public bool playerRide { get; private set; }
    public bool isDestroy { get; private set; }

    private Collider collider;
    private Rigidbody rigidbody;

    private float forwardForce;
    private float forceRate = 2;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        VehicleSlidingSFX = GetComponent<AudioSource>();
        PlaySlidingSFX();
        life = maxLife;
    }

    protected virtual void Update()
    {
        if (playerRide)
        {
            if (life <= 0)
            {
                lifeBecomeZero?.Invoke();
                isDestroy = true;
                Destroy(gameObject);
            }
            else
            {
                life -= Time.deltaTime;
                changeLife?.Invoke(life);
            }
        }  
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * forwardForce * forceRate;
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, direction.z);
    }

    public void Init(float forwardForce)
    {
        this.forwardForce = forwardForce;
    }

    public virtual void ChangePlayerRide(bool value)
    {
        playerRide = value;

        if ( playerRide == false)
        {
            changeLife = null;
            isDestroy = true;
            Disable();
            DisrideEvent?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            collider.enabled = false;
            rigidbody.isKinematic = true;
            BeRideEvent?.Invoke(this);            
        }
    }

    public void PlaySlidingSFX()
    {
        if(VehicleSlidingSFX!=null)
            VehicleSlidingSFX.Play();
    }

    public void ChangeForceRate(float rate)
    {
        forceRate = rate;
    }

    public void Disable()
    {
        collider.enabled = false;
        rigidbody.isKinematic = true;
        enabled = false;
    }

    public void Enable()
    {
        collider.enabled = true;
        rigidbody.isKinematic = false;
        enabled = true;
    }
}
