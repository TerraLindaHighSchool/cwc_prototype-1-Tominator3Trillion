using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject target;
    public float maxSpeed = 200.0f;
    private float activeSpeed = 0.0f;

    GameObject explosionPrefab;

   
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * activeSpeed * Time.deltaTime);
            //lerp activeSpeed to maxSpeed
            activeSpeed = Mathf.Lerp(activeSpeed, maxSpeed, Time.deltaTime);
            
        }
    }

    //on collision
    void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 3.0f);
        Destroy(gameObject);
    }
}
