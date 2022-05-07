
using Mirror;
using System.Collections;
using UnityEngine;
using Mirror.Examples.NetworkRoom;

[RequireComponent(typeof(NetworkTransform))]
public class PlayerController : NetworkBehaviour
{
    public CharacterController characterController;
    public int peredozEnergetic = 5;
    public int Yourenergetic = 0;
    [Header("Flashlight Battery Settings")]
    public GameObject Flashlight;
    [SyncVar(hook = nameof(SetBattery))]
    public float battery = 100;
    public float batteryMax = 100;
    public float removeBatteryValue = 0.05f;
    public float secondToRemoveBaterry = 5f;
    public float removeStamina = 5;

    [Header("Health")]
    public float health = 100;
    public static float maxHealth = 100;

    [Header("Stamina Settings")]
    [SyncVar(hook = nameof(SetStamina))]
    public float stamina = 100;
    public static float StaminaMax = 100f;
    public float removeStaminaValue = 5f;
    public float secondToRemoveStamina = 0.5f;
    public float secondToAddStamina = 0.5f;
    public float addStaminaValue = 3f;

    public float speedChangeStamina = 5;
    public float targetStaminaValue;

    public float minStaminaForRun = 25f;

    private float currentTimeForRemoveStamina = 0f;
    private float currentTimeForAddStamina = 0f;

    [Header("Run Settings")]
    public bool IsRun = false;

    public bool CanRun = true;

    public bool IsHidden = false;

    [Header("Flashlight Settings")]
    private Light flashLight;
    public float minIntensity = 0;
    public float maxIntensity = 5.5f;
    public float deltaIntensity;

    public Inventory Inventory;

    private FirstPersonController controller;

    [Command]
    public void CmdFlashlight()
    {
        RpcOnFlashlight();
        
    }

    public void SetBattery(float _, float newValue)
    {
        var intensity = maxIntensity * (newValue / batteryMax);
        flashLight.intensity = intensity;

        var batteryController = Camera.main.GetComponentInChildren<battaryFlashLightController>();
        if (batteryController != null)
        {
            batteryController.SetBattery(newValue);
        }

    }

    public void SetStamina(float _, float newValue)
    {
        var staminaController = Camera.main.GetComponentInChildren<StaminaViewController>();
        if (staminaController != null)
        {
            staminaController.SetStamina(newValue);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HealthUI.Instance.SetHealth(health);
    }

    private void Start()
    {
        battery = batteryMax;
        flashLight = Flashlight.GetComponentInChildren<Light>(true);

        Inventory = GetComponent<Inventory>();

        controller = GetComponent<FirstPersonController>();
        controller.IsRun += OnChangeRun;
    }

    private void OnDestroy()
    {
        controller.IsRun -= OnChangeRun;
    }

    private void OnChangeRun(bool value)
    {
        IsRun = value;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        StartCoroutine(RemoveBaterryCharge(removeBatteryValue, secondToRemoveBaterry));
    }

    [ClientRpc]
    public void RpcOnFlashlight()
    {
        flashLight.enabled = !flashLight.enabled;
    }

    public void UpdateMaxStamina(float maxStaminaNew)
    {
        if (peredozEnergetic >= Yourenergetic)
        {
            Yourenergetic += 1;
            StaminaMax += maxStaminaNew;
        }
        else
        {
            StaminaMax -= removeStamina;
            Yourenergetic -= 1;
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            NetworkRoomManagerExt.Instance.GoToMenu();
            return;
        }
        if (!isLocalPlayer || characterController == null || !characterController.enabled)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            CmdFlashlight();
        }

        currentTimeForRemoveStamina += Time.deltaTime;
        currentTimeForAddStamina += Time.deltaTime;
        UpdateStamina();
        UpdateRunForStamina();
    }

    private void UpdateRunForStamina()
    {

        if (stamina < minStaminaForRun)
        {
            CanRun = false;
        }
        else if (IsRun && stamina == 0)
        {
            CanRun = false;
            IsRun = false;
        }
        else
        {
            CanRun = true;
        }
    }

    private void UpdateStamina()
    {
        if (currentTimeForRemoveStamina >= secondToRemoveStamina)
        {
            currentTimeForRemoveStamina = 0f;
            if (IsRun)
            {
                if (stamina - removeStaminaValue >= 0)
                    targetStaminaValue = stamina - removeStaminaValue;
            }
        }
        if (currentTimeForAddStamina >= secondToAddStamina)
        {
            currentTimeForAddStamina = 0f;
            if (!IsRun)
            {
                if (stamina + addStaminaValue <= StaminaMax)
                    targetStaminaValue = stamina + addStaminaValue;
            }
        }

        if (targetStaminaValue > stamina)
        {
            stamina += speedChangeStamina * Time.deltaTime;
        }
        else if (targetStaminaValue < stamina)
        {
            stamina -= speedChangeStamina * Time.deltaTime;
        }
    }

    [Command]
    public void AddBaterryServer(float value)
    {
        if (battery < batteryMax)
        {
            battery += value;
        }
    }
    
    public void AddBattery(float value)
    {
        AddBaterryServer(value);
    }

    public IEnumerator RemoveBaterryCharge(float value, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("Removing baterry value: " + value);

            if (battery > 0  && flashLight.enabled)
                battery -= value;
            else
                Debug.Log("The flashlight battery is out");
        }
    }


    void FixedUpdate()
    {

    }
}
