using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusVehicle : VehicleDurity
{
    private float time;
    private float countDown;

    private float timeRange = 10;

    public float value { get; private set; }
    private float valueRange = 100;


    private void Start()
    {
        countDown = Random.Range(0, timeRange);
        time = countDown;
    }

    override protected void Update()
    {
        base.Update();

        if(time <= countDown)
        {
            value = Random.Range(0, valueRange);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
