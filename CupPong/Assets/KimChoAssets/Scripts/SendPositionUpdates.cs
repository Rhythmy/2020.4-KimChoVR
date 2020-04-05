using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPositionUpdates : MonoBehaviour
{

    // Reference ASL Scripts
    public SimpleDemos.TransformObjectViaLocalSpace_Example ASLTransformScript;
    public ASL.ASLObject aslObject;

    // Private Data
    private Vector3 previousPosition;
    private Vector3 previousRotation;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.ASLTransformScript == null)
        {
            this.ASLTransformScript = this.GetComponent<SimpleDemos.TransformObjectViaLocalSpace_Example>();
        }

        if (this.aslObject == null)
        {
            this.aslObject = this.GetComponent<ASL.ASLObject>();
        }

        this.previousPosition = this.transform.position;
        this.previousRotation = this.transform.localEulerAngles;

        //this.ASLTransformScript.m_SendAdditiveTransform = true;

        this.timer = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!aslObject.m_Mine)
        {
            return;
        }

        if (!this.previousPosition.Equals(this.transform.position) && this.timer >= 0.2f ||
            !this.previousRotation.Equals(this.transform.localEulerAngles) && this.timer >= 0.2f)
        {
            // Handle Position
            this.ASLTransformScript.m_MoveToPosition = this.transform.position;
            this.ASLTransformScript.m_SendTransform = true;

            // Handle Scale
            this.ASLTransformScript.m_ScaleToAmount = this.transform.localScale;

            // Handle Rotation
            this.ASLTransformScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
            this.ASLTransformScript.m_MyCustomAxis = this.transform.eulerAngles;

            this.previousPosition = this.transform.position;
            this.previousRotation = this.transform.localEulerAngles;
            this.timer = 0.0f;
        }

        this.timer += Time.fixedDeltaTime;
    }

    /*
     * if (!this.previousPosition.Equals(this.transform.position))
        {
            // Handle Position
            this.ASLTransformScript.m_MoveToPosition = this.transform.position;
            this.ASLTransformScript.m_SendTransform = true;

            // Handle Scale
            this.ASLTransformScript.m_ScaleToAmount = this.transform.localScale;

            // Handle Rotation
            this.ASLTransformScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
            this.ASLTransformScript.m_MyCustomAxis = this.transform.localEulerAngles;
            Debug.Log(this.ASLTransformScript.m_MyCustomAxis);

            this.previousPosition = this.transform.position;
        }
     * 
     */

    /*
     * if (!this.previousPosition.Equals(this.transform.position))
        {
            Vector3 difference = new Vector3(
                this.transform.position.x - this.previousPosition.x,
                this.transform.position.y - this.previousPosition.y,
                this.transform.position.z - this.previousPosition.z);

            float x = Mathf.Round(difference.x * 100f) / 100f;
            float y = Mathf.Round(difference.y * 100f) / 100f;
            float z = Mathf.Round(difference.z * 100f) / 100f;

            ASLTransformScript.m_AdditiveMovementAmount = new Vector3(x, y, z);

            this.previousPosition = this.transform.position;
        }
     * 
     */
}
