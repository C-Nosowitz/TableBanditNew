using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelDialogue : MonoBehaviour
{
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            started = true;
        }

    }
}
