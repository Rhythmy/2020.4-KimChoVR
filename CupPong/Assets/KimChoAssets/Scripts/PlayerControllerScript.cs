using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;
    public CharacterController controller;

    public VRTK.WindowsMR_TrackedObject rHand;
    public VRTK.WindowsMR_TrackedObject lHand;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 move = lHand.GetAxis(UnityEngine.XR.WSA.Input.InteractionSourcePressType.Thumbstick);

        Vector3 vec3Move = transform.right * move.x + transform.forward * move.y;

        if (move.x < -0.2 || move.x > 0.2 || move.y < -0.2 || move.y > 0.2)
        {
            controller.Move(vec3Move * moveSpeed * Time.deltaTime);
        }

        Vector2 rotate = rHand.GetAxis(UnityEngine.XR.WSA.Input.InteractionSourcePressType.Thumbstick);

        if (rotate.x < -0.2 || rotate.x > 0.2)
        {
            this.transform.Rotate(0, rotate.x * rotateSpeed * Time.deltaTime, 0);
        }
    }
}
