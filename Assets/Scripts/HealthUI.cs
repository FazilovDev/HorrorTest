using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image Health;

    public static HealthUI Instance;

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
        SetHealth(100f);
    }

    public void SetHealth(float health)
    {
        if (health < 0 || health > PlayerController.maxHealth)
        {
            return;
        }
        Health.fillAmount = health / PlayerController.maxHealth;
    }
}
