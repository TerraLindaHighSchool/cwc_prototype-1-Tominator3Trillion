using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : MonoBehaviour
{

    public GameObject target;
    public float normalSpeed = 50f;
    public float farSpeed = 500f;
    public float fireRate = 0.5f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Ship");
    }
    // Update is called once per frame
    void Update()
    {
        float s = normalSpeed;
        if (Vector3.Distance(transform.position, target.transform.position) > 500f)
        {
            s = farSpeed;
        }
        //lerp rotation towards target
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime * s);
        //move forward
        transform.Translate(Vector3.forward * s * Time.deltaTime);
        
    }

    //on collision with ship
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.GetComponent<ShipController>().EjectPlayer();
            other.gameObject.SetActive(false);
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            StartCoroutine(gameManager.GetComponent<GameManager>().RespawnShip());
            Destroy(gameObject);
        }
    }
}
