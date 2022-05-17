using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingDetector : MonoBehaviour
{

    public float maxDistance = 10f;

    public RotationalPosition LandingSpot()
    {
        //shoot raycast from local space down direction
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDistance))
        {
           // Debug.Log("hit");
            return new RotationalPosition(hit.point, Quaternion.LookRotation(hit.transform.position - hit.collider.gameObject.transform.position));
        }

        return new RotationalPosition(Vector3.zero, Quaternion.identity);
    }

    
}

public class RotationalPosition
{
    public Vector3 position;
    public Quaternion rotation;


    public RotationalPosition(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
