using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float[] productTimes = new float[3];
    [SerializeField]
    private ResultUI resultUI;
    [SerializeField]
    private GameObject[] sampleVehicles;

    private PlayerControl player;

    private float productTime;
    private int timeIndex = 0;

    private bool isGameEnd = false;

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

    private void Start()
    {
        var vehicle = FindObjectOfType<VehicleDurity>();
        player.Ride(vehicle);
        vehicle.lifeBecomeZero += fail;
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
        var pos = player.transform.position;
        pos.x = Random.Range(-10f, 10f);
        pos.y += 2;
        pos.z -= 5;

        var go = sampleVehicles[Random.Range(0, sampleVehicles.Length)];
        go = Instantiate(go, pos, player.transform.rotation);

        var vehicle = go.GetComponent<VehicleDurity>();
        vehicle.Init(player.GetForwardForce());
        vehicle.lifeBecomeZero += fail;

        StartCoroutine(vehicleCheck(vehicle));
    }

    private IEnumerator vehicleCheck(VehicleDurity vehicle)
    {
        //TODO:
        yield return null;
    }

    private void fail()
    {
        if (isGameEnd) { return; }
        resultUI.Show(false);
        gameEndHandle();
    }

    private void pass()
    {
        if (isGameEnd) { return; }
        resultUI.Show(true);
        gameEndHandle();
    }

    private void gameEndHandle()
    {
        isGameEnd = true;
        player.enabled = false;
    }
}
