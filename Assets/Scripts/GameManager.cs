using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject ship;
    public GameObject fighterPrefab;
    public Transform[] enterAnimationPositions;
    private Transform currentShipPosTarget;
    public Transform fighterSpawnPos;
    public float delay;

    public GameObject menu;
    public GameObject GUI;
    public Camera menuCam;

    public static bool gameStarted = false;
    private bool inAnim = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(!gameStarted && inAnim) {
            ship.transform.position = Vector3.MoveTowards(ship.transform.position, currentShipPosTarget.transform.position, 10 * Time.deltaTime);
            ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, currentShipPosTarget.rotation, 10 * Time.deltaTime);
        }
    }

    public void Play() {
        //enable ship
        ship.SetActive(true);
        StartCoroutine(EnterAnimation());
    }

    IEnumerator EnterAnimation() {
        menu.SetActive(false);
        inAnim=true;
        ship.transform.position = enterAnimationPositions[0].position;
        foreach(Transform t in enterAnimationPositions) {
            currentShipPosTarget = t;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(3);

        Instantiate(fighterPrefab, fighterSpawnPos.position, fighterSpawnPos.rotation);
        gameStarted = true;
        inAnim=false;
        
        menuCam.enabled = false;
        GUI.SetActive(true);
    }

    public IEnumerator RespawnShip() {
        yield return new WaitForSeconds(3);
        player.SetActive(false);
        ship.SetActive(true);
    }
}
