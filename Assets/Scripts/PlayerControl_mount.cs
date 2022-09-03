using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl : MonoBehaviour
{
    private VehicleDurity vehicle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Vechle")
        {
            Ride(other.transform.parent.GetComponent<VehicleDurity>());
        }
    }

    public void Ride(VehicleDurity newVehicle)
    {
        if(newVehicle == vehicle) { return; }

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
