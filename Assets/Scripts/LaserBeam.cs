using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float speed = 10f;

    public GameObject explosionPrefab;
    private int tick = 0;

    // Update is called once per frame
    void Update()
    {
        //move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void FixedUpdate() {
        tick++;
        if (tick > 1000) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        Debug.Log("Laser hit " + other.gameObject.name);
        if(other.gameObject.tag=="Ship" || other.gameObject.tag=="Atmosphere") 
            return;
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 3.0f);
        if (other.gameObject.tag == "Enemy")
        {
            explosion.transform.localScale = new Vector3(5f, 5f, 5f);
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "Boss")
        {
            explosion.transform.localScale = new Vector3(10f, 10f, 10f);
            //get boss script
            Boss bossScript = other.gameObject.GetComponent<Boss>();
            //damage boss
            bossScript.TakeDamage(20f);
            
        }
        Destroy(gameObject); 
    }
}
