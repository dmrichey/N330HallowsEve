using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Vector3 speed;

    private Rigidbody rb;

    public WheelCollider CFR;
    public WheelCollider CFL;
    public WheelCollider CBR;
    public WheelCollider CBL;

    public GameObject WFR;
    public GameObject WFL;
    public GameObject WBR;
    public GameObject WBL;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateWheels();
        rb.AddForce(speed);
    }

    void UpdateWheels()
    {

        Vector3 pos;
        Quaternion rot;
        CFR.GetWorldPose(out pos, out rot);
        WFR.transform.rotation = rot;
        WFR.transform.position = pos;

        CFL.GetWorldPose(out pos, out rot);
        WFL.transform.rotation = rot;
        WFL.transform.position = pos;

        CBR.GetWorldPose(out pos, out rot);
        WBR.transform.rotation = rot;
        WBR.transform.position = pos;

        CBL.GetWorldPose(out pos, out rot);
        WBL.transform.rotation = rot;
        WBL.transform.position = pos;

    }
}
