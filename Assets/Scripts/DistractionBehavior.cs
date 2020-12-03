using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistractionBehavior : MonoBehaviour
{
    public Transform distractSpot;
    public NavMeshAgent agent;
    public float distractionRadius = 35f;
    private bool isDistracting = true;
    public Animator anim;

    void OnEnable()
    {
        isDistracting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            anim.SetTrigger("Move");
            CheckSurroundings();
        }

        if (!isDistracting)
        {
            CheckSurroundings();
            gameObject.SetActive(false);
        }
    }

    void CheckSurroundings()
    {
        Collider[] otherObjectsInRadius = Physics.OverlapSphere(transform.position, distractionRadius);

        foreach (var hitCollider in otherObjectsInRadius)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                if (isDistracting)
                    hitCollider.GetComponent<Enemy>().SetDistraction(distractSpot);
                else
                    hitCollider.GetComponent<Enemy>().RemoveDistractions();
            }
        }

        
    }

    public void RemoveDistractions()
    {
        isDistracting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hint" && gameObject.name == "Skwawks")
        {
            RemoveDistractions();
        }
    }
}
