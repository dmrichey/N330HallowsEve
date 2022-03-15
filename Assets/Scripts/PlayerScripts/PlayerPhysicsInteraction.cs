using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsInteraction : MonoBehaviour
{

    public float forceMag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnControllerColliderHit(ControllerColliderHit Hit)
    {
        Rigidbody rb = Hit.collider.attachedRigidbody;

        if(rb != null)
        {
            Vector3 forceDir = Hit.gameObject.transform.position - transform.position;
            forceDir.y = 0;
            forceDir.Normalize();

            rb.AddForceAtPosition(forceDir * forceMag, transform.position, ForceMode.Impulse);
        }

    }
}
