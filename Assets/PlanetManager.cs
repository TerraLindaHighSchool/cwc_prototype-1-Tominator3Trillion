using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanetManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

    //health bar slider
    public Slider healthBar;

    public GameObject explosion;

    private int tick = 0;

    public void Damage(int damage) {
        health -= damage;
        healthBar.value = health / maxHealth;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        GameObject explosion = Instantiate(this.explosion, transform.position, transform.rotation);

        PlayerPrefs.SetInt("Highscore", tick);
        
        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene() {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start() {
        health = maxHealth;
        healthBar.value = health / maxHealth;
    }

    void Update() {
        if (health <= 0) {
            Die();
        }
    }

    public void Heal(int heal) {
        health += heal;
        if(health > maxHealth) {
            health = maxHealth;
        }
        healthBar.value = health / maxHealth;
    }

    void FixedUpdate() {
        if (!GameManager.gameStarted) {
            return;
        }

        if (tick % 5000 == 0) {
            maxHealth += 1;
        }

        if (tick % 1000 == 0) {
            Heal((int)(maxHealth/10));
        }
        tick++;
    }



}
