using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class ObjectController : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusChangedHandler
{
    // Scripts
    public SimpleDemos.SendFloatArray_Example floatObject;
    public SimpleDemos.TransformObjectViaLocalSpace_Example ASLTransformScript;

    // GameObjects
    //public GameObject objectToFollow;

    // Flags
    public bool leftGrab;
    public bool rightGrab;
    public bool attachToHand;

    // Private Data
    private Vector3 previousPosition;
    private Vector3 previousRotation;
    
    void Start()
    {
        this.previousPosition = this.transform.position;
        this.previousRotation = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        handleObjectKinematic();

        // If the object is ours, then do nothing with it.
        if (this.gameObject.GetComponent<ASL.ASLObject>().m_Mine)
        {
            return;
        }
        
        sendTransformUpdates();

    }

    public void sendTransformUpdates()
    {
        if (!this.previousPosition.Equals(this.transform.position) ||
            !this.previousRotation.Equals(this.transform.localEulerAngles))
        {
            // Handle Position
            this.ASLTransformScript.m_MoveToPosition = this.transform.position;

            // Handle Scale
            this.ASLTransformScript.m_ScaleToAmount = this.transform.localScale;

            // Handle Rotation
            this.ASLTransformScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
            this.ASLTransformScript.m_MyCustomAxis = this.transform.eulerAngles;

            this.previousPosition = this.transform.position;
            this.previousRotation = this.transform.localEulerAngles;

            this.ASLTransformScript.m_SendTransform = true;
        }
    }

    public void makeKinematic()
    {
        this.floatObject.m_MyFloats[0] = 1.0f;
        this.floatObject.m_SendFloat = true;
    }

    public void releaseKinematic()
    {
        this.floatObject.m_MyFloats[0] = 0.0f;
        this.floatObject.m_SendFloat = true;
    }

    public void handleObjectKinematic()
    {
        if (floatObject != null)
        {
            if (this.gameObject.GetComponent<Collider>() != null)
            {
                if (this.gameObject.GetComponent<Rigidbody>() != null)
                {
                    if (floatObject.m_MyFloats[0] > 0.9 && floatObject.m_MyFloats[0] < 1.1)
                    {
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    } else
                    {
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    }
                }
            }
        }
    }

    public void ClaimMe()
    {
        if (this.gameObject.GetComponent<ASL.ASLObject>() != null)
        {
            if (!this.gameObject.gameObject.GetComponent<ASL.ASLObject>().m_Mine)
            {
                this.gameObject.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    Debug.Log("Successfully claimed object! " + this.gameObject.name);
                });
            }
            else
            {
                Debug.Log("Already own this object");
            }
        }
    }

    public void OnBeforeFocusChange(FocusEventData eventData)
    {
    }

    public void OnFocusChanged(FocusEventData eventData)
    {
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (floatObject == null)
        {
            return;
        }

        if (eventData != null)
        {
            if (eventData.Pointer.Controller.ControllerHandedness == Handedness.Left)
            {
                leftGrab = true;
            }

            if (eventData.Pointer.Controller.ControllerHandedness == Handedness.Right)
            {
                rightGrab = true;
            }

            floatObject.m_MyFloats[0] = 1;
            if (attachToHand)
            {
                this.transform.position = eventData.Pointer.Position;
            }
        }
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (floatObject == null)
        {
            return;
        }

        if (floatObject.m_MyFloats[0] == 1)
        {
            if (attachToHand)
            {
                this.transform.position = eventData.Pointer.Position;
            }
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (floatObject == null)
        {
            return;
        }

        if (eventData.Pointer.Controller.ControllerHandedness == Handedness.Left)
        {
            leftGrab = false;
        }

        if (eventData.Pointer.Controller.ControllerHandedness == Handedness.Right)
        {
            rightGrab = false;
        }

        if (!leftGrab && !rightGrab)
        {
            floatObject.m_MyFloats[0] = 0;
        }
    }
}
