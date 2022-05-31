using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private float elapsed = 100f;
    public float duration = 0.1f;
    public float magnitude = 1.0f;

    private float x;
    private float y;

    

    // Update is called once per frame
    void Update()
    {
        //get the main camera
        Camera mainCamera = Camera.main;
        
        Vector3 originalPos = mainCamera.transform.localPosition;
        
        //if elapsed reaches duration
        if (elapsed >= duration)
        {
            //reset elapsed
            elapsed = 0.0f;
            //generate new random values
            x = Random.Range(-1.0f, 1.0f) * magnitude;
            y = Random.Range(-1.0f, 1.0f) * magnitude;
        }
        elapsed += Time.deltaTime;

        //lerp main camera position to new position
        mainCamera.transform.localPosition = Vector3.Lerp(originalPos, new Vector3(x, y, originalPos.z), elapsed / duration);


    }
}
