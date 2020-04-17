using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLObjectSync : MonoBehaviour
{
    public GameObject ASLObjectToSpawn;
    private GameObject objectToSyncWith;
    private ObjectController objectController;

    // Start is called before the first frame update
    void Start()
    {
        objectController = this.gameObject.GetComponent<ObjectController>();
        objectToSyncWith = Instantiate(ASLObjectToSpawn);
        objectToSyncWith.transform.position = objectController.gameObject.transform.position;
        objectToSyncWith.transform.rotation = objectController.gameObject.transform.rotation;
        objectController.objectToSyncWith = objectToSyncWith;
        objectController.GetComponent<SimpleDemos.TransformObjectViaLocalSpace_Example>().m_ObjectToManipulate = objectToSyncWith;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
