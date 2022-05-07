using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PointScreamer
{
    public Transform Point;
    public bool IsPlayMusic = false;
    public bool IsStopMusic = false;

}

public class RunningScreamer : MonoBehaviour
{

    public List<PointScreamer> Points;

    public AudioClip ChaseClip;
    public GameObject ScreamerObject;
    private int currentIndex = 0;
    private Transform targetPoint;
  
    private AudioSource audioSource;
    public bool StopToFinish;

    public float Speed;
   
    public bool IsAppear { get; private set; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartScreamer();
        }
    }
    private void StartScreamer()
    {
            var children = ScreamerObject.transform.GetChild(0);
            children.gameObject.SetActive(true);
    }
    public void Appear()
    {
        IsAppear = true;
        targetPoint = Points[currentIndex].Point;
        audioSource.clip = ChaseClip;
        UpdateMusic();
    }

    private void Hide()
    {
        Destroy(gameObject);
    }

    private void UpdateMusic()
    {
        if (Points[currentIndex].IsPlayMusic)
        {
            audioSource.Play();
        }
        else if (Points[currentIndex].IsStopMusic)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (IsAppear)
        {
            var distance = Vector3.Distance(transform.position, targetPoint.position);
            if (distance < 0.1f && currentIndex < Points.Count)
            {
                currentIndex++;
                if (currentIndex == Points.Count)
                {
                    Hide();
                    return;
                }
                targetPoint = Points[currentIndex].Point;
                UpdateMusic();
            }


            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, Speed * Time.deltaTime);
        }
    }
}
