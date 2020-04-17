using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(parent);
    }
}
