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
            
        if(gameTick %1000 == 0)
        {
            GameObject fighter = Instantiate(fighterPrefab, transform.position, transform.rotation);
        }
        gameTick++;
    }

    void Die()
    {
        GameObject explosion = Instantiate(massiveExplosion, transform.position, transform.rotation);
        Destroy(explosion, 10.0f);
        Destroy(gameObject);
    }
}
