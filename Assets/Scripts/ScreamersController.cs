using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreamersController : MonoBehaviour
{
    public Image Screamer;
    public AudioClip clip;
    public AudioSource audioSource;

    private float timeScreamer = 1f;
    private float time;
    private bool isActivate = false;

    public void Activate()
    {
        isActivate = true;
        Screamer.enabled = true;
        time = 0f;
        audioSource.PlayOneShot(clip);
    }

    private void Deactivate()
    {
        isActivate = false;
        Screamer.enabled = false;
    }

    private void Update()
    {
        if (isActivate)
        {
            time += Time.deltaTime;
            if (time >= timeScreamer)
            {
                Deactivate();
            }
        }
    }
}
