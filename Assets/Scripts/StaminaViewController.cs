using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaViewController : MonoBehaviour
{
    public Image StaminaImage;

    public static StaminaViewController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        SetStamina(100f);
    }

    public void SetStamina(float stamina)
    {
        if (stamina < 0 || stamina > PlayerController.StaminaMax)
        {
            return;
        }
        StaminaImage.fillAmount = stamina / PlayerController.StaminaMax;
    }
}
