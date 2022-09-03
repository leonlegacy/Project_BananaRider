using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl : MonoBehaviour
{
    private VehicleDurity vehicle;

    public void Ride(VehicleDurity newVehicle)
    {
        if(vehicle != null)
        {
            vehicle.ChangePlayerRide(false);
        }

        newVehicle.ChangePlayerRide(true);

        var pos = newVehicle.transform.position;
        pos.y += 1;

        transform.position = pos;
        newVehicle.transform.parent = transform;

        vehicle = newVehicle;
    }
}
