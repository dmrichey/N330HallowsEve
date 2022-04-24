using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class InvisibilityUI : MonoBehaviour
{

    
    public Slider slider;
    public static float newValue;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.m_isInvisible)
        {
            newValue = PlayerController.m_invisTimer / PlayerController.m_invisCooldown;
            slider.value = newValue;
        }

        if (PlayerController.m_isInvisible)
        {
            newValue = PlayerController.m_invisTimer / PlayerController.m_invisDuration;
            slider.value = newValue;
        }

    }
}
