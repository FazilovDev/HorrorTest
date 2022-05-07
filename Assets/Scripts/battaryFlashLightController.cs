using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battaryFlashLightController : MonoBehaviour
{
    public Image batteryImage;
    public static battaryFlashLightController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private float maxBattery = 100f;

    private void Start()
    {
        SetBattery(100f);
        
    }

    public void SetBattery(float battery)
    {
        batteryImage.fillAmount = battery / 100f;
    }
}
