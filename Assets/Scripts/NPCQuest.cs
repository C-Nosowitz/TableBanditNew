using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCQuest : MonoBehaviour
{
    public GameObject player;
    public InventoryObj inventory;
    public int requiredItemAmount;
    public GameObject gate;
    public GameObject gateIcon;
    public NavMeshAgent distraction;
    public Vector3 distractSpot;
    public Text itemCounter;
    private bool firstTime = true;

    public AudioSource peopleTalkingAudio;

    public Dialogue dialogue;
    public bool seen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<BanditsMovement>().GetInventoryCount() == requiredItemAmount)
        {
            if (gameObject.name == "Yuri")
            {
                if (Vector3.Distance(distraction.transform.position, distractSpot) <= 2f)
                {
                    //Debug.Log("ready");
                    clearBerries();
                    distraction.gameObject.GetComponent<DistractionBehavior>().distractionRadius = 500f;
                    peopleTalkingAudio.gameObject.SetActive(false);
                }
            }

            if (gameObject.name == "Skwawks")
            {
                clearBerries();
            }
            
        }
    }

    public void ShowNumberNeeded()
    {
        if (firstTime)
        {
            itemCounter.text = "/" + requiredItemAmount;
            firstTime = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!firstTime)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                if (player.GetComponent<BanditsMovement>().GetInventoryCount() == requiredItemAmount)
                {
                    GetComponent<DialogueTrigger>().dialogue = dialogue;
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    gate.SetActive(false);

                    gateIcon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                    //distraction.gameObject.SetActive(true);
                    distraction.SetDestination(distractSpot);
                }
            }
            
        }
    }

 void clearBerries()
    {
        if (player.GetComponent<BanditsMovement>().GetInventoryCount() == requiredItemAmount)
        {
            inventory.Container.Clear();

            if (!player.GetComponent<LookAhead>().textBox.activeSelf)
            {
                
            }
        }
    }

}
