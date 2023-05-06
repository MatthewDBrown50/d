using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool hasPackage = false;
    private float destroyPackageDelay = 0.1f;
    private Color32 hasPackageColor = new Color32(0, 195, 15, 255);
    private Color32 noPackageColor = new Color32(255,255,255,255);
    private float slowMoveSpeed;
    private float normalMoveSpeed;
    private float fastMoveSpeed;
    private bool speedIsModified;
    private float speedChangeDelay;
    private float speedChangeDelayMonitor;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        normalMoveSpeed = playerMovement.getMoveSpeed();
        slowMoveSpeed = normalMoveSpeed * .5f;
        fastMoveSpeed = normalMoveSpeed + (normalMoveSpeed * .7f);

        speedChangeDelay = 3f;
        speedChangeDelayMonitor = -1;
    }

    private void Update()
    {
        if (speedIsModified)
        {
            speedChangeDelayMonitor = 0f;
            speedIsModified = false;
        }
        else if (speedChangeDelayMonitor >= speedChangeDelay) { 
            playerMovement.setMoveSpeed(normalMoveSpeed);
            speedChangeDelayMonitor = -1;
        } 
        else if (speedChangeDelayMonitor >= 0)
        {
            speedChangeDelayMonitor += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
    }

    private void OnTriggerEnter2D(Collider2D triggerObject)
    {
        if(triggerObject.tag == "Package" && !hasPackage)
        {
            Debug.Log("Got the package!");
            hasPackage = true;

            // Completely remove the package object from the game
            Destroy(triggerObject.gameObject, destroyPackageDelay);

            // Turn car's color to color that indicates we have a package
            spriteRenderer.color = hasPackageColor;
        } 
        else if(triggerObject.tag == "Customer" && hasPackage)
        {
            Debug.Log("Delivered the package!");
            hasPackage = false;

            // Revert car's color to default color to indicate we do not have a package
            spriteRenderer.color = noPackageColor;
        }
        else if(triggerObject.tag == "SpeedBoost")
        {
            playerMovement.setMoveSpeed(fastMoveSpeed);
            speedIsModified = true;
        }
        else if(triggerObject.tag == "SpeedDrop")
        {
            playerMovement.setMoveSpeed(slowMoveSpeed);
            speedIsModified = true;
        }
    }

}
