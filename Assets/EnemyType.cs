using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        type = Mathf.Abs(Random.Range(-3, 4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
