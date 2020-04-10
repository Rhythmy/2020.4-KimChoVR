using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public ASL.ASLObject thisASLObject;
    public ASL.ASLObject aslObjectToSyncWith;
    public GameObject objectToSyncWith;
    public SimpleDemos.SendFloatArray_Example floatObject;

    // Update is called once per frame
    void Update()
    {

        handleObjectKinematic();

        // If the object is ours, then do nothing with it.
        if (thisASLObject.m_Mine)
        {
            return;
        }

        // If the object is not ours, have it sync with the connected local object.
        this.transform.position = objectToSyncWith.transform.position;
        this.transform.rotation = objectToSyncWith.transform.rotation;
        this.transform.localScale = objectToSyncWith.transform.localScale + (objectToSyncWith.transform.localScale * 0.1f);
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
}
