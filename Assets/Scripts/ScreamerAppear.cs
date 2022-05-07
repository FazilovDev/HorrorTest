using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerAppear : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioScreamer;

    public GameObject Screamer;

    public bool IsAppear = false;

    public float TimerCooldown = 5f;
    private float currentTime = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!IsAppear)
        {
            return;
        }

        currentTime += Time.deltaTime;
        if (currentTime > TimerCooldown)
        {
            IsAppear = false;
            currentTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !IsAppear)
        {
            audioSource.PlayOneShot(audioScreamer);
            var go = Instantiate(Screamer, transform.position, Quaternion.identity);
            Destroy(go, 1.5f);
            IsAppear = true;
        }
    }
}
