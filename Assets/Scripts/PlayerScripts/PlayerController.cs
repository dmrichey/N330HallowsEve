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
    public GameObject       grayscale;
    public GameObject[] enemies = null;

    Vector3                 m_movement;
    Quaternion              m_rotation = Quaternion.identity;
    bool                    m_isMoving;

    public static float     m_invisDuration = 5.0f;
    public static float     m_invisCooldown = 15.0f;
    public static float     m_invisTimer = 0.0f;
    public static bool      m_isInvisible;

    public static float     m_thermDuration = 5.0f;
    public static float     m_thermCooldown = 10.0f;
    public static float     m_thermTimer = 0.0f;    
    public static bool      m_thermalVision;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_char = GetComponent<CharacterController>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Timer to handle invisibility
        m_invisTimer += Time.deltaTime;
        m_thermTimer += Time.deltaTime;

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
                SetInvisible(m_isInvisible);
            }
        }

        if (m_invisTimer >= m_invisDuration && m_isInvisible)
        {
            m_isInvisible = false;
            SetInvisible(m_isInvisible);
        }

        // -- HANDLE THERMAL VISION
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_thermTimer >= m_thermCooldown)
            {
                m_thermalVision = true;
                m_thermTimer = 0.0f;
                SetThermal(m_thermalVision);
            }
        }

        if (m_thermTimer >= m_thermDuration && m_thermalVision)
        {
            m_thermalVision = false;
            SetThermal(m_thermalVision);
        }
    }

    void SetInvisible(bool state)
    {
        mesh.SetActive(!state);
        invisMesh.SetActive(state);
    }

    void SetThermal(bool state)
    {
        grayscale.SetActive(state);
        // Find All Enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            // Toggle Meshes
            enemies[i].GetComponent<EnemyType>().ToggleThermal(state);
        }

    }
}
