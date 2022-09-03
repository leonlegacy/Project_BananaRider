using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float[] productTimes = new float[3];

    private PlayerControl player;

    private float productTime;
    private int timeIndex = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        
        var obstacles = FindObjectsOfType<TouchObstacle>();
        foreach(var ob in obstacles)
        {
            ob.touchObstacle += fail;
        }

        var endPoint = FindObjectOfType<FinishLineDetector>();
        endPoint.hitFinishLine += pass;

        productTime = productTimes[timeIndex];
    }

    private void Update()
    {
        productTime -= Time.deltaTime;

        if(productTime <= 0 && timeIndex < productTimes.Length)
        {
            productVehicle();
            timeIndex++;
            if(timeIndex < productTimes.Length)
            {
                productTime = productTimes[timeIndex];
            }
        }
    }

    private void productVehicle()
    {
        //TODO
    }

    private void fail()
    {
        //TODO
    }

    private void pass()
    {
        //TODO
    }
}
