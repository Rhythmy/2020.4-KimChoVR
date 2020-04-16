using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class ObjectController : MonoBehaviour
{
    // Scripts
    public SimpleDemos.SendFloatArray_Example floatObject;
    public SimpleDemos.TransformObjectViaLocalSpace_Example ASLTransformScript;

    // GameObjects
    public GameObject objectToFollow;

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

        if (!this.gameObject.GetComponent<ASL.ASLObject>().m_Mine)
        {
            return;
        }

        if (objectToFollow != null)
        {
            sendTransformUpdates();
        }
    }

    public void sendTransformUpdates()
    {
        // Handle Position
        this.ASLTransformScript.m_MoveToPosition = objectToFollow.transform.position;

        // Handle Scale
        this.ASLTransformScript.m_ScaleToAmount = this.transform.localScale;

        // Handle Rotation
        this.ASLTransformScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
        this.ASLTransformScript.m_MyCustomAxis = objectToFollow.transform.eulerAngles;

        this.previousPosition = objectToFollow.transform.position;
        this.previousRotation = objectToFollow.transform.localEulerAngles;

        this.ASLTransformScript.m_SendTransform = true;
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
                    if (objectToFollow != null)
                    {
                        floatObject.m_MyFloats[0] = 1.0f;
                    } else
                    {
                        floatObject.m_MyFloats[0] = 0.0f;
                    }

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
}
