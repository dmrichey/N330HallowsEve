using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public int type;

    public GameObject torch;
    public GameObject pitchfork;

    private Animator anim;

    public GameObject maleMesh;
    public GameObject thermalMesh;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        type = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 0)
        {
            torch.SetActive(false);
            pitchfork.SetActive(true);

            anim.SetFloat("weaponType", 0.0f);
        }

        if (type == 1)
        {
            torch.SetActive(true);
            pitchfork.SetActive(false);

            anim.SetFloat("weaponType", 1.0f);
        }

        if (type == 2)
        {
            torch.SetActive(false);
            pitchfork.SetActive(true);

            anim.SetFloat("weaponType", 0.0f);
        }

        if (type == 3)
        {
            torch.SetActive(true);
            pitchfork.SetActive(false);

            anim.SetFloat("weaponType", 1.0f);
        }
    }

    public void ToggleThermal(bool state)
    {
        thermalMesh.SetActive(state);
    }
}
