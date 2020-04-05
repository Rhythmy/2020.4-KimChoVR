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
            Debug.Log("Collided with server object attempt claim!");
            otherCollider.gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                Debug.Log("Successfully claimed object!");
            });
        }
    }
}
