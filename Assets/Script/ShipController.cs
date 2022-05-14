﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed=5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAccerleration = 2.5f, strafeAccerleration = 2f, hoverAccerleration = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAccerleration = 3.5f;

    public LandingDetector landingDetector;
    private bool isLanding = false;
    private bool landed = false;
    private Vector3 landingSpot;
    private Quaternion landingRotation;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        

        if(!landed) {
            if(!isLanding) {
                lookInput.x = Input.mousePosition.x;
                lookInput.y = Input.mousePosition.y;

                
                mouseDistance.x = (lookInput.x - screenCenter.x)/screenCenter.y ;
                mouseDistance.y = (lookInput.y - screenCenter.y)/screenCenter.y;

                mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1);

                rollInput = Mathf.Lerp(rollInput, -Input.GetAxis("Roll"), Time.deltaTime * rollAccerleration);

                transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

                activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccerleration * Time.deltaTime);
                activeStrafeSpeed =  Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAccerleration * Time.deltaTime);
                activeHoverSpeed =  Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccerleration * Time.deltaTime);

                transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
                transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
            
            
                // check if p is pressed
                if (Input.GetKeyDown(KeyCode.P) && !isLanding)
                {
                    RotationalPosition landingSpotInfo = landingDetector.LandingSpot();
                    landingSpot = landingSpotInfo.position;
                    landingRotation = landingSpotInfo.rotation;

                    Debug.Log("Landing spot: " + landingSpot);

                    if(landingSpot != Vector3.zero)
                    {
                        isLanding = true;
                        ToggleChildrenColliders(false);
                    }
                }
            }

            if(isLanding)
            {
                Debug.Log("Landing");
                LandingProcedure();
            }
        } else {
            //if space is pressed
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //reset the game
                landed = false;
                isLanding = false;
                
                activeHoverSpeed = 100f;
            }
        }

        //set velocity to rigidbody velocity to zero
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    
    }

    private void LandingProcedure() {
        // move toward the landing spot at a constant rate
        transform.position = Vector3.MoveTowards(transform.position, landingSpot, 10 * Time.deltaTime);
        //rotate toward to look forward
        Quaternion targetRotation = landingRotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
            
        //if within a certain distance and rotation set the position and rotation to the landing spot
        if(Vector3.Distance(transform.position, landingSpot) < 0.1f && Quaternion.Angle(transform.rotation, targetRotation) < 5f)
        {
            transform.position = landingSpot;
            transform.rotation = targetRotation;
            landed = true;

            ToggleChildrenColliders(true);

            //spawn the landing particles
                
            //spawn the player
        }
    }

    private void ToggleChildrenColliders(bool toggle) {
        foreach(Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = toggle;
        }
    }

    //on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyable"))
        {
            Destroy(other.gameObject);
        } else {
            Debug.Log("Collided with " + other.gameObject.name);
        }
    }

}