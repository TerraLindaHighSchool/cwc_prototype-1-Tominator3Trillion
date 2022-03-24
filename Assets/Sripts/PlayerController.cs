using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float speed = 20f;


    public GameObject[] tires = new GameObject[4];

    void Update()
    {
        //get input from horizontal and vertical axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        //rotate tires
        foreach(GameObject tire in tires)
        {
            tire.transform.Rotate(Vector3.right * verticalInput * -speed * 10* Time.deltaTime);

            //set rotation on the y axis to horizontal input times turn speed
            tire.transform.rotation = Quaternion.Euler(tire.transform.rotation.eulerAngles.x, horizontalInput * turnSpeed + transform.rotation.eulerAngles.y, tire.transform.rotation.eulerAngles.z);
        }


        //move forward or back
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);

        //rotate left or right
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}
