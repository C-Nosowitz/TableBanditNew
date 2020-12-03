using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkwaksControl : MonoBehaviour
{
    public GameObject skwaks;
    public float flightDistance;
    public float timeDistracting;
    private bool inUse = false;
    private int usesLeft = 3;
    private float timer;
    public Transform player;
    public Text skwaksCounter;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeDistracting;
    }

    // Update is called once per frame
    void Update()
    {
        if (usesLeft > 0 && !inUse)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skwaks.SetActive(true);
                skwaks.transform.position = player.position + (player.forward * 2);
                NavMeshAgent agent = skwaks.GetComponent<NavMeshAgent>();
                Vector3 target = player.position + (transform.forward * flightDistance);
                agent.SetDestination(target);
                inUse = true;
                timer = timeDistracting;
                usesLeft--;
                skwaksCounter.text = "x" + usesLeft;
            }
        }
        else if (inUse)
        {
            //Debug.Log(usesLeft);
            if (timer <= 0)
            {
                skwaks.GetComponent<DistractionBehavior>().RemoveDistractions();
                //skwaks.SetActive(false);
                inUse = false;
            }
            else
            {
                timer -= Time.deltaTime;
                //Debug.Log(timer);
            }
        }
    }
}
