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
        this.gameObject.GetComponent<SimpleDemos.CreateObject_Example>().m_SpawnObject = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
