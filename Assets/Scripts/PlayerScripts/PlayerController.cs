using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float runSpeed = 2f;
    public float walkSpeed = 1f;
    public float currentSpeed;
    public Transform camera;

    Animator                m_anim;
    CharacterController     m_char;
    public GameObject       mesh;
    public GameObject       invisMesh;
    Vector3                 m_movement;
    Quaternion              m_rotation = Quaternion.identity;
    bool                    m_isMoving;
    float                   m_invisDuration = 5.0f;
    float                   m_invisCooldown = 30.0f;
    float                   m_invisTimer = 0.0f;
    bool                    m_isInvisible;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_char = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Timer to handle invisibility
        m_invisTimer += Time.deltaTime;

        // -- HANDLE MOVEMENT
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        m_movement.Set(horizontal, 0.0f, vertical);
        m_movement.Normalize();

        if (m_movement.magnitude >= 0.1f)
        {
            m_isMoving = true;
        } else
        {
            m_isMoving = false;
        }

        currentSpeed = runSpeed;

        // Move speed and Animations
        if (m_isMoving)
        {
            m_anim.SetInteger("AnimState", 1);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_anim.SetBool("IsDashing", true);
                currentSpeed = runSpeed;
            } else
            {
                m_anim.SetBool("IsDashing", false);
                currentSpeed = walkSpeed;
            }
        } else
        {
            m_anim.SetInteger("AnimState", 0);
        }

        if (m_isMoving)
        {
            float targetAngle = Mathf.Atan2(m_movement.x, m_movement.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            moveDir.y -= 9.81f * Time.deltaTime;
            m_char.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        

        // -- HANDLE INVISIBILITY
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_invisTimer >= m_invisCooldown)
            {
                m_isInvisible = true;
                m_invisTimer = 0.0f;
            }
        }

        if (m_invisTimer >= m_invisDuration)
        {
            m_isInvisible = false;
        }

        if (m_isInvisible)
        {
            mesh.SetActive(false);
            invisMesh.SetActive(true);
        } else
        {
            mesh.SetActive(true);
            invisMesh.SetActive(false );
        }
    }
}
