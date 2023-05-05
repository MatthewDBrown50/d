using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool hasPackage = false;
   
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
        } 
        else if(triggerObject.tag == "Customer" && hasPackage)
        {
            Debug.Log("Delivered the package!");
            hasPackage = false;
        }
    }
}
