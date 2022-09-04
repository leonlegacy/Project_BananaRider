using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusVehicle : VehicleDurity
{
    public System.Action<float> Struggle;

    private float time;
    private float countDown;

    private float timeRange = 10;

    public float value { get; private set; }
    private float valueRange = 5;

    private void Start()
    {
        countDown = Random.Range(2, timeRange);
        time = countDown;
    }

    override protected void Update()
    {
        base.Update();

        if(time <= 0)
        {
            value = Random.Range(-valueRange, valueRange);
            Struggle?.Invoke(value);
            countDown = Random.Range(2, timeRange);
            time = countDown;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public override void ChangePlayerRide(bool value)
    {
        if (!value)
        {
            Struggle = null;
        }
        base.ChangePlayerRide(value);
    }
}
