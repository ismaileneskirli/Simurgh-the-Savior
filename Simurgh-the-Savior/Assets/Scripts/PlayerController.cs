using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // vars for moving the player
    float xOffset ;
    float yOffset ;
    float xThrow;
    float yThrow;
    [SerializeField] float controlSpeed = 15f;

    //var for restricting player movement
    [SerializeField] float xRange = 12f;
    [SerializeField] float yRange = 2.5f;
    // Start is called before the first frame update
    // pitch = eğim
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField]float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 10f;
    [SerializeField] float controlRollFactor = -10f;

    [SerializeField]  GameObject[] lasers;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl  = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControl = xThrow * controlRollFactor;

        // Quaternion related to rotation
        float pitch = pitchDueToControl + pitchDueToPosition;
        float yaw = yawDueToPosition;
        float roll = rollDueToControl;
        //transform.localRotation = Quaternion.Euler(x, y, z);
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void ProcessTranslation()
    {
        // XPOS
        xThrow = Input.GetAxis("Horizontal");
        xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        // restricts the movement with given range.
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        // YPOS
        yThrow = Input.GetAxis("Vertical");
        yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange + 10f);


        // Change the position with given values.
        transform.localPosition = new Vector3
        (clampedXPos,
         clampedYPos,
        transform.localPosition.z);
    }


    void ProcessFiring(){
            if(Input.GetButton("Fire1")){
                FireLasers(true);
            }
            else {
                FireLasers(false);
            }
    }


    private void FireLasers(bool isActive)
    {
        foreach (GameObject laser in lasers){
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }
}