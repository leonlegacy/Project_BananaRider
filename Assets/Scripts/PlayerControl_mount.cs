using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl : MonoBehaviour
{
    public Action<VehicleDurity> RideEvent;

    private VehicleDurity vehicle;

    public void Ride(VehicleDurity newVehicle)
    {
        if (newVehicle == vehicle) { return; }
        if (newVehicle.isDestroy) { return; }

        if (vehicle != null)
        {
            vehicle.ChangePlayerRide(false);
        }

        newVehicle.ChangePlayerRide(true);

        var pos = newVehicle.transform.position;
        pos.y += 1;

        transform.position = pos;
        newVehicle.transform.parent = playerCharacter;
        //newVehicle.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        newVehicle.transform.localRotation = Quaternion.identity; 
        vehicle = newVehicle;

        if(vehicle is OctopusVehicle)
        {
            (vehicle as OctopusVehicle).Struggle += SetOctupusForce;
        }

        RideEvent?.Invoke(vehicle);
    }
}
