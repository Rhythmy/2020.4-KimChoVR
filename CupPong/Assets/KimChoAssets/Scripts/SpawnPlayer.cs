using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public SimpleDemos.CreateObject_Example spawnScript;

    // On start, creates players avatar
    void Start()
    {
        spawnScript = this.gameObject.GetComponent<SimpleDemos.CreateObject_Example>();

        spawnScript.m_CreateObject = SimpleDemos.CreateObject_Example.ObjectToCreate.CapsulePlayer;
        spawnScript.m_SpawnObject = true;
    }
}
