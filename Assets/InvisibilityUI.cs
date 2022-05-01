using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class InvisibilityUI : MonoBehaviour
{

    
    public Image invisSlider;
    public Image thermSlider;

    float newValue;
    
    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.m_isInvisible)
        {
            newValue = PlayerController.m_invisTimer / PlayerController.m_invisCooldown;
            invisSlider.fillAmount = newValue;
        } else 
        {
            newValue = 1 - ( PlayerController.m_invisTimer / PlayerController.m_invisDuration );
            invisSlider.fillAmount = newValue;
        }

        if (!PlayerController.m_thermalVision)
        {
            newValue = PlayerController.m_thermTimer / PlayerController.m_thermCooldown;
            thermSlider.fillAmount = newValue;
        } else
        {
            newValue = 1 - (PlayerController.m_thermTimer / PlayerController.m_thermDuration);
            thermSlider.fillAmount = newValue;
        }

    }
}
