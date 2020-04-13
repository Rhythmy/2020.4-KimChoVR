using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarScript : MonoBehaviour
{
    public ASL.ASLObject playerASL;
    public SimpleDemos.TransformObjectViaLocalSpace_Example sendPositionScript;
    public GameObject playerToSyncWith;

    // Private Data
    private Vector3 previousPosition;
    private Vector3 previousRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerASL = this.gameObject.GetComponent<ASL.ASLObject>();
        sendPositionScript = this.gameObject.GetComponent<SimpleDemos.TransformObjectViaLocalSpace_Example>();
        playerToSyncWith = GameObject.FindGameObjectWithTag("MainCamera");

        sendPositionScript.m_ObjectToManipulate = this.gameObject;

        this.previousPosition = playerToSyncWith.transform.position;
        this.previousRotation = playerToSyncWith.transform.localEulerAngles;

        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerASL.m_Mine)
        {
            return;
        }

        if (!this.previousPosition.Equals(playerToSyncWith.transform.position) ||
            !this.previousRotation.Equals(playerToSyncWith.transform.localEulerAngles))
        {
            // Handle Position
            this.sendPositionScript.m_MoveToPosition = playerToSyncWith.transform.position;

            // Handle Scale
            this.sendPositionScript.m_ScaleToAmount = new Vector3(0.4f, 0.4f, 0.4f);

            // Handle Rotation
            this.sendPositionScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
            this.sendPositionScript.m_MyCustomAxis = playerToSyncWith.transform.eulerAngles;

            this.previousPosition = playerToSyncWith.transform.position;
            this.previousRotation = playerToSyncWith.transform.localEulerAngles;

            this.sendPositionScript.m_SendTransform = true;
        }
    }
}
