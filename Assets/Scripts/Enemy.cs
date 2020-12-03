using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool isPatrolling;
    public string spotName;
    protected bool backToSpot;
    protected bool outsideBoundary;

    //public CharacterController controller;
    public Animator anim;
    public NavMeshAgent agent;
    public float speed;
    public float damping;
    public float chaseSpeed;
    public LayerMask mask;
    public LayerMask mask1;
    public LayerMask mask2;

    protected bool chase;
    public float distanceAway;
    public float timerMin;
    public float timerMax;
    public bool collision;
    public float proximityAwareness;
    public float lineOfSight;
    public float peripheralVision;
    public Transform eyes;
    private Vector3 eyePosition;

    public Transform player;
    public GameManager gameManager;

    protected bool pause;
    public float pauseTimer;

    protected bool sightObscured;
    protected bool isDistracted;

    public AudioSource footstepSounds;

    public Enemy(bool doesPatrol)
    {
        isPatrolling = doesPatrol;
        //controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = chaseSpeed;
        pause = false;
        backToSpot = false;
        outsideBoundary = false;
        chase = false;
        sightObscured = false;
    }

    //-------------------------Sight-------------------------------------
    public void OnDogSpotted()
    {
        anim.ResetTrigger("StartChase");
        Debug.Log("Spotted!");
        pause = false;
        backToSpot = false;
        chase = true;
        anim.SetTrigger("StartChase");
        anim.SetBool("Chase", true);
        anim.SetBool("Pause", false);
        sightObscured = false;
    }

    public void EnemySight()
    {
        //Debug.Log("sight");
        //enemy vision
        eyePosition = eyes.position;

        Vector3 targetDir = player.position - eyePosition;
        //Debug.Log(eyes.forward);
        float angle = Vector3.Angle(targetDir, eyes.forward);

        Vector3 sight = Quaternion.AngleAxis(30, eyes.right) * eyes.forward;
        Vector3 left = Quaternion.AngleAxis(-30, eyes.up) * sight;
        Vector3 right = Quaternion.AngleAxis(30, eyes.up) * sight;
        //Debug.Log(angle);
        if (angle < 60f)
        {
            //creating enemy's vision
            RaycastHit hitForward;
            RaycastHit hitLeft;
            RaycastHit hitRight;

            Debug.DrawRay(eyePosition, sight * lineOfSight, Color.green);
            Debug.DrawRay(eyePosition, left * peripheralVision, Color.red);
            Debug.DrawRay(eyePosition, right * peripheralVision, Color.red);
            
            if (Physics.Raycast(eyePosition, sight, out hitForward, lineOfSight, ~mask))
            {
               // Debug.Log(hitForward.collider.name);
                if (hitForward.collider.CompareTag("Player"))
                {
                    //Debug.Log("aaaaa");
                    OnDogSpotted();
                }
                    
               // else
                   // Debug.Log("There's an obstacle in the way");
            }
            else if (Physics.Raycast(eyePosition, sight, out hitLeft, peripheralVision, ~mask))
            {
                if (hitLeft.collider.CompareTag("Player"))
                    OnDogSpotted();
               // else
                   // Debug.Log("There's an obstacle in the way");
            }
            else if (Physics.Raycast(eyePosition, sight, out hitRight, peripheralVision, ~mask))
            {
                if (hitRight.collider.CompareTag("Player"))
                    OnDogSpotted();
               // else
                   // Debug.Log("There's an obstacle in the way");
            }
        }
    }

    public void DetectObstacles()
    {
        //Debug.Log("detecting");
        //var forward = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        var rayDirection = player.position - transform.position;
        Debug.DrawRay(eyes.position, rayDirection * proximityAwareness, Color.blue);

        //if encountering an obstacle
        if (Physics.Raycast(eyes.position, rayDirection, out hit, proximityAwareness, ~mask))
        {
            //Debug.Log(hit.collider.tag);
            if (!(hit.collider.CompareTag("Player")))
            {
                //Debug.Log("hidden");
                sightObscured = true;
            }
            else
                sightObscured = false;
            
        }
        /*else
        {
            //Debug.Log("visible");
            sightObscured = false;
        }*/
    }

    public void EnemyChase()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist <= distanceAway && sightObscured == false)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
            Debug.Log("chase over");
            anim.ResetTrigger("StartChase");
            chase = false;
            anim.SetBool("Chase", false);
            pause = true;
            anim.SetBool("Pause", true);
            pauseTimer = SetTimer();
        }
    }

    public void SetDistraction(Transform distraction)
    {
        agent.SetDestination(distraction.position);
        isDistracted = true;
        pause = false;
        
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 0.5f)
        {
            anim.SetBool("Pause", true);
            anim.SetBool("Distracted", false);
            // Debug.Log("hi");
        }
        else
            anim.SetBool("Pause", false);
    }

    public void RemoveDistractions()
    {
        Debug.Log("Distraction gone");
        isDistracted = false;
        pause = true;
        anim.SetBool("Pause", true);
        anim.SetBool("Distracted", false);
        pauseTimer = 1.0f;
        backToSpot = true;
    }

    //-------------------------------Timer------------------------------
    public float SetTimer()
    {
        float timer = Random.Range(timerMin, timerMax);
       // Debug.Log("Timer set at " + timer);
        return timer;
    }

    //------------------------------Collisions----------------------------
    void OnTriggerEnter(Collider other)
    {
        
        //if it collides with anything other than these specific objects
        if (other.gameObject.tag != "ground")
        {
            
            if (isPatrolling)
            {
                if (other.gameObject.tag == "Boundary")
                {
                    if (chase == false)
                        collision = true;
                    if (outsideBoundary)
                        outsideBoundary = false;
                    else
                        outsideBoundary = true;
                }
            }

            if (other.gameObject.tag == spotName && backToSpot)
            {
                //Debug.Log("howdy");
                backToSpot = false;
                agent.ResetPath();
            }

            if (other.gameObject.CompareTag("Player"))
            {
                agent.ResetPath();
                anim.ResetTrigger("StartChase");
                chase = false;
                anim.SetBool("Chase", false);
                pause = true;
                anim.SetBool("Pause", true);
                pauseTimer = 0.5f;
                other.enabled = false;
                other.gameObject.GetComponent<Animator>().SetTrigger("Caught");
                gameManager.LoseLife();
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        collision = false;        
    }

}