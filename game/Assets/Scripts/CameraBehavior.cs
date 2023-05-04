using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] GameObject playerCar;
    
    // The movement of the car is updated in the Update() method of the PlayerMovement script
    // To avoid camera jitters, we should ensure that the camera is updated AFTER the car's postion is updated
    // We accomplish this by placing the camera tracking in the LateUpdate() method
    void LateUpdate()
    {
        // player.transform.position is a Vector3, and is positioned EXACTLY where the car is. 
        // This positioning means that all we see is the background if we match the camera to it.
        // We back the camera up by adding some negative number to the Z axis via a new Vector3
        transform.position = playerCar.transform.position + new Vector3(0,0,-10);
    }
}
