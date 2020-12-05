using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    public GameObject talkPrompt;
    public GameObject textBox;
    public int displayTimer;

    public float sightDistance;
    RaycastHit hitInfo;

    public bool levelEnd;

    // Start is called before the first frame update
    void Start()
    {
        talkPrompt.SetActive(false);
        //textBox.SetActive(false);

        levelEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!textBox.activeSelf)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, sightDistance + 1))
            {
                if (hitInfo.collider.gameObject.tag == "NPC")
                {
                    displayTimer = 30;
                    talkPrompt.SetActive(true);
                    hitInfo.collider.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
            }
        }

            if (talkPrompt.activeSelf)
            {
                displayTimer--;
                if (Input.GetKeyDown("t"))
                {
                     textBox.SetActive(true);
                     displayTimer = 0;
                    if (hitInfo.collider.name != "Yuri" && hitInfo.collider.name != "Skwawks")
                        levelEnd = true;
                    else if (hitInfo.collider.name == "Yuri" || hitInfo.collider.name == "Skwawks")
                    {
                        hitInfo.collider.gameObject.GetComponent<NPCQuest>().ShowNumberNeeded();
                    }
                }
                if (displayTimer == 0)
                {
                    talkPrompt.SetActive(false);
                }//end display after a certain amount of time
            }


        }

    private void OnTriggerEnter(Collider other)
    {
        var dialogue = other.GetComponent<DialogueTrigger>();
        Debug.Log(dialogue);
        if (dialogue && !dialogue.seen)
        {
            dialogue.TriggerDialogue();
            textBox.SetActive(true);
            dialogue.seen = true;
        }
    }

}
