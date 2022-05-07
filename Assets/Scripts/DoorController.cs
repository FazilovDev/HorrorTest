using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private GameObject doorModel;
    [SerializeField] private float openedAngle;
    [SerializeField] private float closedAngle;
    [SerializeField] private float timeAction;

    [SerializeField] private ItemType requiredKey;
    public string RequiredMessage;

    private float targetAngle;
    private float startAngle;
    private float currentAngle;
    private float t;
    private bool isRotate = false;

    [Header("Door Status")]
    public bool IsOpened;
    public bool IsLocked;

    private AudioSource audioSource;
    [SerializeField] private AudioClip audioOpen;
    [SerializeField] private AudioClip audioClose;
    [SerializeField] private AudioClip audioLocked;
    [SerializeField] private AudioClip audioUnlocked;

    private void Awake()
    {
        currentAngle = doorModel.transform.localRotation.eulerAngles.y;
        audioSource = GetComponent<AudioSource>();
    }

    public void UnLock()
    {
        IsLocked = false;
        audioSource.PlayOneShot(audioUnlocked);
    }

    public void Action(GameObject gameObject)
    {
        if (IsLocked)
        {
            return;
        }

        IsOpened = !IsOpened;
        if (IsOpened)
        {
            Open();
            audioSource.PlayOneShot(audioOpen);
        }
        else
        {
            Close();
            audioSource.PlayOneShot(audioClose);
        }
    }

    public void OpenByMale()
    {
        if (!IsOpened)
        {
            Open();
            IsOpened = true;
            audioSource.PlayOneShot(audioOpen);
        }
    }

    public void CloseByMale()
    {
        if (IsOpened)
        {
            Close();
            IsOpened = false;
            audioSource.PlayOneShot(audioClose);
        }
    }

    private void Open()
    {
        isRotate = true;
        targetAngle = openedAngle;
        startAngle = closedAngle;
        if (!Mathf.Approximately(t, 0))
        {
            t = 1f - t;
        }
    }

    private void Close()
    {
        isRotate = true;
        targetAngle = closedAngle;
        startAngle = openedAngle;
        if (!Mathf.Approximately(t, 0))
        {
            t = 1f - t;
        }
    }

    private void Update()
    {
        if (!isRotate || Mathf.Approximately(currentAngle, targetAngle))
        {
            isRotate = false;
            return;
        }

        currentAngle = Mathf.Lerp(startAngle, targetAngle, t);
        doorModel.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

        t += Time.deltaTime * 1f / timeAction;

        if (t >= 1)
        {
            t = 0;
            currentAngle = targetAngle;
            doorModel.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
            isRotate = false;
        }
    }
}
