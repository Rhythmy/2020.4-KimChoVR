using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarScript : MonoBehaviour
{
    public enum AvatarType
    {
        Head,
        Body,
        Left,
        Right
    }

    public ASL.ASLObject playerASL;
    public SimpleDemos.TransformObjectViaLocalSpace_Example sendPositionScript;
    public GameObject avatarPartToSyncWith;
    public AvatarType avatar;
    public MeshRenderer mesh;

    // Private Data
    private Vector3 previousPosition;
    private Vector3 previousRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerASL = this.gameObject.GetComponent<ASL.ASLObject>();
        sendPositionScript = this.gameObject.GetComponent<SimpleDemos.TransformObjectViaLocalSpace_Example>();

        if (avatar == AvatarType.Body) {
            avatarPartToSyncWith = GameObject.FindGameObjectWithTag("MainCamera");
        }
        else if (avatar == AvatarType.Left)
        {
            avatarPartToSyncWith = GameObject.FindGameObjectWithTag("LeftController");
        }
        else if (avatar == AvatarType.Right)
        {
            avatarPartToSyncWith = GameObject.FindGameObjectWithTag("RightController");
        }

        sendPositionScript.m_ObjectToManipulate = this.gameObject;

        this.previousPosition = avatarPartToSyncWith.transform.position;
        this.previousRotation = avatarPartToSyncWith.transform.localEulerAngles;

        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (avatarPartToSyncWith == null)
        {
            if (avatar == AvatarType.Body)
            {
                avatarPartToSyncWith = GameObject.FindGameObjectWithTag("MainCamera");
            }
            else if (avatar == AvatarType.Left)
            {
                avatarPartToSyncWith = GameObject.FindGameObjectWithTag("LeftController");
            }
            else if (avatar == AvatarType.Right)
            {
                avatarPartToSyncWith = GameObject.FindGameObjectWithTag("RightController");
            }
            return;
        }

        if (!playerASL.m_Mine)
        {
            mesh.gameObject.SetActive(true);
            return;
        }

        if (avatar != AvatarType.Body)
        {
            mesh.gameObject.SetActive(false);
        }

        if (!this.previousPosition.Equals(avatarPartToSyncWith.transform.position) ||
            !this.previousRotation.Equals(avatarPartToSyncWith.transform.localEulerAngles))
        {
            if (avatar == AvatarType.Body)
            {
                SendBodyUpdates();
            } else if (avatar == AvatarType.Left || avatar == AvatarType.Right)
            {
                SendControllerUpdates();
            }
        }
    }

    void SendBodyUpdates()
    {
        // Handle Position
        this.sendPositionScript.m_MoveToPosition = avatarPartToSyncWith.transform.position;

        // Handle Scale
        this.sendPositionScript.m_ScaleToAmount = new Vector3(0.4f, 0.4f, 0.4f);

        // Handle Rotation
        this.sendPositionScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
        this.sendPositionScript.m_MyCustomAxis = avatarPartToSyncWith.transform.eulerAngles;

        this.previousPosition = avatarPartToSyncWith.transform.position;
        this.previousRotation = avatarPartToSyncWith.transform.localEulerAngles;

        this.sendPositionScript.m_SendTransform = true;
    }

    void SendControllerUpdates()
    {
        // Handle Position
        this.sendPositionScript.m_MoveToPosition = avatarPartToSyncWith.transform.position;

        // Handle Rotation
        this.sendPositionScript.m_MyRotationAxis = SimpleDemos.TransformObjectViaLocalSpace_Example.RotationAxis.custom;
        this.sendPositionScript.m_MyCustomAxis = avatarPartToSyncWith.transform.eulerAngles;

        this.previousPosition = avatarPartToSyncWith.transform.position;
        this.previousRotation = avatarPartToSyncWith.transform.localEulerAngles;

        this.sendPositionScript.m_SendTransform = true;
    }
}
