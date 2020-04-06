using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<ASL.ASLObject>() != null)
        {
            if (otherCollider.tag == "GameController")
            {
                if (!otherCollider.gameObject.GetComponent<ASL.ASLObject>().m_Mine)
                {
                    otherCollider.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Debug.Log("Successfully claimed object! " + otherCollider.name);
                    });
                }
                else
                {
                    Debug.Log("Already own this object");
                }
            }
        }
    }

    void OnTriggerStay(Collider otherCollider)
    {
        if (otherCollider.GetComponent<ASL.ASLObject>() != null)
        {
            if (otherCollider.tag == "GameController")
            {
                if (!otherCollider.gameObject.GetComponent<ASL.ASLObject>().m_Mine)
                {
                    otherCollider.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Debug.Log("Successfully claimed object! " + otherCollider.name);
                    });
                } else
                {
                    Debug.Log("Already own this object");
                }
            }
        }
    }
}
