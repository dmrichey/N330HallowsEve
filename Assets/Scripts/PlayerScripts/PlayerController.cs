using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 10.0f;
    public float runSpeed = .2f;
    public float walkSpeed = .1f;
    public float currentSpeed;

    Animator    m_anim;
    Rigidbody   m_rb;
    Vector3     m_movement;
    Quaternion  m_rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_movement.Set(horizontal, 0.0f, vertical);
        m_movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0.0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0.0f);
        bool isMoving = hasHorizontalInput || hasVerticalInput;

        currentSpeed = runSpeed;

        if (isMoving)
        {
            m_anim.SetInteger("AnimState", 1);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            } else
            {
                currentSpeed = walkSpeed;
            }
        } else
        {
            m_anim.SetInteger("AnimState", 0);
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_movement, turnSpeed * Time.deltaTime, 0.0f);
        m_rotation = Quaternion.LookRotation(desiredForward);

        this.transform.position += (m_movement * currentSpeed);
        m_rb.MoveRotation(m_rotation);
    }
}
