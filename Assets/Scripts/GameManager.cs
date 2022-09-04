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
    [SerializeField]
    private int productCount = 3;
    [SerializeField]
    private UIController uiController;

    private PlayerControl player;

    private float productTime;
    private int timeIndex = 0;

    private bool isGameEnd = false;

    private Dictionary<VehicleDurity, Coroutine> vehicleCoroutineDic = new Dictionary<VehicleDurity, Coroutine>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        player.RideEvent += rideHandle;
        player.DropHole += fail;

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
            StartCoroutine(productCycle());
            timeIndex++;
            if(timeIndex < productTimes.Length)
            {
                productTime = productTimes[timeIndex];
            }
        }
    }

    private void rideHandle(VehicleDurity vehicle)
    {
        uiController.InitLifeBar(vehicle.maxLife);
        vehicle.changeLife += uiController.SetVehicleLife;
    }

    private IEnumerator productCycle()
    {
        var count = productCount;

        do
        {
            yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
            count--;
            productVehicle();
        } while (count > 0);
    }

    private void productVehicle()
    {
        var pos = player.transform.position;
        pos.x = Random.Range(-10f, 10f);
        pos.y += 2;
        pos.z -= 8;

        var go = sampleVehicles[Random.Range(0, sampleVehicles.Length)];
        go = Instantiate(go, pos, player.transform.rotation);

        var vehicle = go.GetComponent<VehicleDurity>();
        vehicle.Init(player.GetForwardForce());
        vehicle.lifeBecomeZero += fail;
        vehicle.BeRideEvent += vehicleDecheck;
        vehicle.DisrideEvent += vehicleDecheck;

        var corutine = StartCoroutine(vehicleCheck(vehicle));
        vehicleCoroutineDic.Add(vehicle, corutine);
    }

    private IEnumerator vehicleCheck(VehicleDurity vehicle)
    {
        do
        {
            yield return null;
        } while (vehicle.transform.position.z - player.transform.position.z < 1);

        vehicle.ChangeForceRate(1);

        yield return new WaitForSeconds(8);

        vehicle.ChangeForceRate(10);

        vehicleCoroutineDic.Remove(vehicle);
    }

    private void vehicleDecheck(VehicleDurity vehicle)
    {
        if (vehicleCoroutineDic.TryGetValue(vehicle, out var cor))
        {
            if(cor != null)
            {
                StopCoroutine(cor);
            }
            vehicleCoroutineDic.Remove(vehicle);
        }
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
        player.Finish();
        player.enabled = false;
    }
}
