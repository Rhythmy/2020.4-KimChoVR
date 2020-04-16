using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class PlayerHandScript : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject grabbedObject;
    public Handedness handedness;

    // Flags
    private bool isTriggerDown;

    private void Update()
    {
        if (handedness == Handedness.Left && Input.GetKey(KeyCode.JoystickButton4))
        {
            isTriggerDown = true;
        }
        else if (handedness == Handedness.Right && Input.GetKey(KeyCode.JoystickButton5))
        {
            isTriggerDown = true;
        } 
        else
        {
            isTriggerDown = false;
        }

        if (grabbedObject != null && grabbedObject.GetComponent<ObjectController>() != null)
        {
            grabbedObject.GetComponent<ObjectController>().objectToFollow = this.gameObject;
        }

        if (!isTriggerDown)
        {
            if (grabbedObject != null && grabbedObject.GetComponent<ObjectController>() != null)
            {
                grabbedObject.GetComponent<ThrowingScript>().simulateThrow();
                grabbedObject.GetComponent<ObjectController>().objectToFollow = null;
            }
            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ENTERED: " + other.gameObject);

        if (other.tag == "Grabbable")
        {
            if (other.GetComponent<ObjectController>() != null)
            {
                selectedObject = other.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("STAY: " + other.gameObject);

        if (other.gameObject == null) {
            selectedObject = null;
        }

        if (handedness == Handedness.Left)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                grabbedObject = selectedObject;
                grabbedObject.GetComponent<ObjectController>().ClaimMe();
            }
        }
        else if (handedness == Handedness.Right)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                grabbedObject = selectedObject;
                grabbedObject.GetComponent<ObjectController>().ClaimMe();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("EXIT: " + other.gameObject);

        if (other.gameObject == selectedObject)
        {
            selectedObject = null;
        }
    }
}
