using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 50)] public float m_speed = 20;
    const string c_horizontalAxis = "Horizontal";
    const string c_verticalAxis = "Vertical";

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis(c_horizontalAxis), 0, Input.GetAxis(c_verticalAxis));
        bool isMoving = false;

        if (movement.sqrMagnitude > 0.0001f)
        {
            isMoving = true;
            movement = movement.normalized;
        }

        if (isMoving)
        {
            transform.Translate(movement * (m_speed * Time.deltaTime));
        }
    }
}
