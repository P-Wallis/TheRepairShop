using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 50)] public float m_speed = 20;
    const string c_horizontalAxis = "Horizontal";
    const string c_verticalAxis = "Vertical";
    public Transform m_animObject;
    private Animator m_animator;
    public float m_turnSpeed = 10f;
    private bool m_wasWalking = false;

    private void Start()
    {
        if (m_animObject != null)
            m_animator = m_animObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis(c_horizontalAxis), 0, Input.GetAxis(c_verticalAxis));
        bool isMoving = false;

        if (movement.sqrMagnitude > 0.0001f)
        {
            isMoving = true;
            movement = movement.normalized;
            if (m_animObject != null)
            {
                float rot = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
                m_animObject.localRotation = Quaternion.Lerp(m_animObject.localRotation, Quaternion.Euler(0, rot, 0), Time.deltaTime * m_turnSpeed);
            }
            if (!m_wasWalking && m_animator)
            {
                m_animator.SetBool("IsWalking", true);
                m_wasWalking = true;
            }
        } else if (m_wasWalking && m_animator)
        {
            m_animator.SetBool("IsWalking", false);
            m_wasWalking = false;
        }

        if (isMoving)
        {
            transform.Translate(movement * (m_speed * Time.deltaTime));
        }
    }
}
