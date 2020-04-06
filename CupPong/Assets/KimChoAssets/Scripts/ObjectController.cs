using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public ASL.ASLObject thisASLObject;
    public ASL.ASLObject aslObjectToSyncWith;
    public GameObject objectToSyncWith;

    // Update is called once per frame
    void Update()
    {
        // If the object is ours, then do nothing with it.
        if (thisASLObject.m_Mine)
        {
            return;
        }

        // If the object is not ours, have it sync with the connected local object.
        this.transform.position = objectToSyncWith.transform.position;
        this.transform.rotation = objectToSyncWith.transform.rotation;
        this.transform.localScale = objectToSyncWith.transform.localScale;
    }
}
