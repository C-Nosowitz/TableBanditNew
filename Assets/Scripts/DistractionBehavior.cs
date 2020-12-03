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

    void OnEnable()
    {
        isDistracting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
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
                //Debug.Log(hitCollider.gameObject.name);
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
}
