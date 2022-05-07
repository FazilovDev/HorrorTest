using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public bool QuestComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "QuestComp")
        {
            QuestComplete = true;
        }
    }
}
