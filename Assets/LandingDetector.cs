using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingDetector : MonoBehaviour
{

    public float radius = 0.5f;
    public float height = 0.5f;

    public Vector3 LandingSpot()
    {
        Vector3 landingSpot = transform.position;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, Vector3.down, out hit, height))
        {
            if (hit.collider.gameObject.tag != "CelestialBody")
            {
                return Vector3.zero;
            }
            landingSpot = hit.point;
        }

        //new raycast to get closest point on the surface
        RaycastHit hit2;
        if (Physics.Raycast(landingSpot, Vector3.down, out hit2, height))
        {
            landingSpot = hit2.point;
        }

        //add 1 to the y value to get the top of the landing spot
        landingSpot.y += 1;


        return landingSpot;
    }

    
}
