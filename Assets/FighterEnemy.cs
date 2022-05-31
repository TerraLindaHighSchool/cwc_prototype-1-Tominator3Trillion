using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : MonoBehaviour
{

    public GameObject target;
    public float speed = 10f;
    public float fireRate = 0.5f;


    // Update is called once per frame
    void Update()
    {
        //lerp rotation towards target
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * speed);
        //move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }
}
