﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleDemos
{
    /// <summary> A simple demo showcasing different ways you can spawn an ASL object</summary>
    public class CreateObject_Example : MonoBehaviour
    {
        /// <summary>
        /// Different objects you can create in this tutorial. Not all variations are listed here.
        /// See documentation for all variations
        /// </summary>
        public enum ObjectToCreate
        {
            CapsulePlayer,
            GizmoLeft,
            GizmoRight,
            PingPongBall
        }

        /// <summary>The object type that will be created</summary>
        public ObjectToCreate m_CreateObject;

        /// <summary> Toggle for creating an object</summary>
        public bool m_SpawnObject = false;

        /// <summary> Handle to the latest Full Prefab object created</summary>
        private static List<GameObject> m_HandleToFreshObjects = new List<GameObject>();
        private static bool doingCallback = false;

        /// <summary>  Holds the rotation of our object so it gets updated properly - see Transform example for better explanation</summary>
        private Quaternion m_RotationHolder;

        /// <summary>Initialize values</summary>
        private void Start()
        {
            m_RotationHolder = Quaternion.identity;
        }

        /// <summary> Scene Logic</summary>
        void Update()
        {
            if (m_SpawnObject)
            {
                m_SpawnObject = false; //Reset to false to prevent multiple unwanted spawns

                if (m_CreateObject == ObjectToCreate.CapsulePlayer)
                {
                    ASL.ASLHelper.InstanitateASLObject("Capsule_Player",
                        new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, "", "UnityEngine.Rigidbody,UnityEngine",
                        WhatToDoWithMyOtherGameObjectNowThatItIsCreated,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);
                } else if (m_CreateObject == ObjectToCreate.GizmoLeft)
                {
                    ASL.ASLHelper.InstanitateASLObject("GizmoLeft",
                        new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, "", "UnityEngine.Rigidbody,UnityEngine",
                        WhatToDoWithMyOtherGameObjectNowThatItIsCreated,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);
                } else if (m_CreateObject == ObjectToCreate.GizmoRight)
                {
                    ASL.ASLHelper.InstanitateASLObject("GizmoRight",
                        new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, "", "UnityEngine.Rigidbody,UnityEngine",
                        WhatToDoWithMyOtherGameObjectNowThatItIsCreated,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);
                }
                else if (m_CreateObject == ObjectToCreate.PingPongBall)
                {
                    ASL.ASLHelper.InstanitateASLObject("PingPongBallPrefab",
                        new Vector3(1, 1, 1), Quaternion.identity, "InteractiveContainer", "",
                        WhatToDoWithMyOtherGameObjectNowThatItIsCreated,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);

                    ASL.ASLHelper.InstanitateASLObject("ASLSyncObject",
                        new Vector3(1, 1, 1), Quaternion.identity, "InteractiveContainer", "",
                        WhatToDoWithMyOtherGameObjectNowThatItIsCreated,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);
                }
            }
        }

        /// <summary>
        /// This function is how you get a handle to the object you just created
        /// </summary>
        /// <param name="_myGameObject">A handle to the gameobject that was just created</param>
        public void WhatToDoWithMyGameObjectNowThatItIsCreated(GameObject _myGameObject)
        {
              //Change the color
            _myGameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                _myGameObject.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(new Color(0.7830189f, 0.3792925f, 03324135f, 1), new Color(0, 0, 0));
            });

            //Send floats to show MyFloatsFunction got set properly
            _myGameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                float[] myFloats = { 1, 2, 3, 4 };
                _myGameObject.GetComponent<ASL.ASLObject>().SendFloatArray(myFloats);
            });
        }

        /// <summary>
        /// A function that is called right after an ASL game object is created if that object was passed in the class name and function name of this function.
        /// This is called immediately upon creation, allowing the user a way to access their newly created object after the server has spawned it
        /// </summary>
        /// <param name="_gameObject">The gameobject that was created</param>
        public static void WhatToDoWithMyOtherGameObjectNowThatItIsCreated(GameObject _gameObject)
        {
            //An example of how we can get a handle to our object that we just created but want to use later
            m_HandleToFreshObjects.Add(_gameObject);

            _gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                Debug.Log("Created and claimed");
            });
        }

        /// <summary>
        /// A function that is called when an ASL object's claim is rejected. This function can be set to be called upon object creation.
        /// </summary>
        /// <param name="_id">The id of the object who's claim was rejected</param>
        /// <param name="_cancelledCallbacks">The amount of claim callbacks that were cancelled</param>
        public static void ClaimRecoveryFunction(string _id, int _cancelledCallbacks)
        {
            Debug.Log("Aw man. My claim got rejected for my object with id: " + _id + " it had " + _cancelledCallbacks + " claim callbacks to execute.");
            //If I can't have this object, no one can. (An example of how to get the object we were unable to claim based on its ID and then perform an action). Obviously,
            //deleting the object wouldn't be very nice to whoever prevented your claim
            if (ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out ASL.ASLObject _myObject))
            {
                _myObject.GetComponent<ASL.ASLObject>().DeleteObject();
            }

        }

        /// <summary>
        /// A function that is called whenever an ASL object calls <see cref="ASL.ASLObject.SendFloatArray(float[])"/>.
        /// This function can be assigned to an ASL object upon creation.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_myFloats"></param>
        public static void MyFloatsFunction(string _id, float[] _myFloats)
        {
            Debug.Log("The floats that were sent are:\n");
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(_myFloats[i] + "\n");
            }
        }

    }
}