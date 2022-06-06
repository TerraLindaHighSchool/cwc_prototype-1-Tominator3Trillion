using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject laser;
    public float health = 100f;
    public GameObject massiveExplosion;

    public GameObject fighterPrefab;

    private int gameTick = 0;

    private int maxEnemies = 2;

    private bool laserShooting = true;


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }



    void FixedUpdate()
    {
        if(!GameManager.gameStarted)
            return;

        if(gameTick % 5000 == 0) {
            maxEnemies += 1;
        }
            
        if(gameTick %1000 == 0)
        {
            GameObject fighter = Instantiate(fighterPrefab, transform.position, transform.rotation);
        }
        gameTick++;

        if(laserShooting && gameTick % 100 == 0) {
            laser.SetActive(true);
            //find object in scene with planet manager
            GameObject planetManager = GameObject.Find("PlanetManager");
            //get planet manager script
            PlanetManager planetManagerScript = planetManager.GetComponent<PlanetManager>();
            //damage planet manager
            planetManagerScript.Damage(1);
        }
    }

    void Die()
    {
        GameObject explosion = Instantiate(massiveExplosion, transform.position, transform.rotation);
        Destroy(explosion, 10.0f);
        Destroy(gameObject);
    }
}
