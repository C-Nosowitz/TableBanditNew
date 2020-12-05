using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EndingControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform shadow;
    public Vector3 startpos;
    public Transform hand;
    public DialogueManager dManager;
    public int startLineNum;
    public int pickupLineNum;
    public int endLineNum;
    private bool moved = false;
    private bool pickuped = false;
    private void Update()
    {
        if (dManager.lineNumber >= startLineNum && !moved)
        {
            agent.SetDestination(shadow.position);
            moved = true;
        }

        if (dManager.lineNumber >= pickupLineNum && !pickuped)
        {
            anim.SetTrigger("PickUp");
            pickuped = true;
        }

        if (dManager.lineNumber >= endLineNum && pickuped)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void PickUpCat()
    {
        shadow.parent = hand;
        shadow.localPosition = Vector3.zero;
    }

    public void End()
    {
        agent.isStopped = false;
        agent.SetDestination(startpos);
        anim.SetBool("Pause", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Shadow")
        {
            anim.SetBool("Pause", true);
            agent.isStopped = true;
        }
            
    }
}
