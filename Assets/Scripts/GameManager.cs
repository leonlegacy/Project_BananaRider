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

    private float gameTime;

    private Dictionary<VehicleDurity, Coroutine> vehicleCoroutineDic = new Dictionary<VehicleDurity, Coroutine>();

    private enum Status
    {
        Ready,
        Gaming,
        End
    }

    private Status status = Status.Ready;

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

        var cameraFollower = GetComponent<CameraFollowScript>();
        var vfxTriggers = FindObjectsOfType<VFX_Trigger>();
        foreach (var triggers in vfxTriggers)
        {
            triggers.SmallViewEvent += cameraFollower.SetSmallViewFX;
            triggers.WaterViewEvent += cameraFollower.SetWaterViewFX;
            triggers.ThirdPersonEvent += cameraFollower.SetThirdPersonFX;
        }

        var endPoint = FindObjectOfType<FinishLineDetector>();
        endPoint.hitFinishLine += pass;

        productTime = productTimes[timeIndex];
    }

    private void Start()
    {
        var vehicle = FindObjectOfType<VehicleDurity>();
        vehicle.Disable();
        player.Disable();

        StartCoroutine(ready());
    }

    private void Update()
    {
        switch (status)
        {
            case Status.Gaming:
                {
                    gameTime += Time.deltaTime;
                    uiController.SetTimeText((int)gameTime);

                    productTime -= Time.deltaTime;

                    if (productTime <= 0 && timeIndex < productTimes.Length)
                    {
                        StartCoroutine(productCycle());
                        timeIndex++;
                        if (timeIndex < productTimes.Length)
                        {
                            productTime = productTimes[timeIndex];
                        }
                    }
                }
                break;
        }
    }

    private IEnumerator ready()
    {
        yield return new WaitForSeconds(3);

        var vehicle = FindObjectOfType<VehicleDurity>(true);
        player.Enable();
        vehicle.Enable();
        player.Ride(vehicle);
        vehicle.lifeBecomeZero += fail;
        status = Status.Gaming;
    }

    private void rideHandle(VehicleDurity vehicle)
    {
        uiController.InitLifeBar(vehicle.maxLife);
        uiController.SetIcon(vehicle.icon);
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
        if (status == Status.End) { return; }
        resultUI.Show(false);
        gameEndHandle();
    }

    private void pass()
    {
        if (status == Status.End) { return; }
        resultUI.Show(true);
        gameEndHandle();
    }

    private void gameEndHandle()
    {
        status = Status.End;
        player.Disable();
        player.enabled = false;

        foreach(var v in FindObjectsOfType<VehicleDurity>())
        {
            v.Disable();
        }
    }
}
