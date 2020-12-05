using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : Enemy
{
    //this script creates simple Enemy AI where the enemy will move around
    //and avoid obstacles and actively look for player
    private float moveTimer;
    public float direction;
    public float turnDirection;
    public Transform spot;
    public float returnSpeed;


    public SimpleMovement(bool doesPatrol) : base(doesPatrol)
    {
        direction = 0;
        transform.eulerAngles = new Vector3(0, direction, 0);
        moveTimer = SetTimer();
    }

    private void Start()
    {
        footstepSounds.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
     {
        if (!pause)
        {
            //Debug.Log("play footsteps");
            footstepSounds.gameObject.SetActive(true);
        }
        else
        {
            footstepSounds.gameObject.SetActive(false);
        }

         if (chase)
         {
            //Debug.Log("chasing");
            DetectObstacles();
            //if (outsideBoundary
            backToSpot = true;
            EnemyChase();
         }
         else if (backToSpot && pause == false)
         {
            //Debug.Log("return");
            ReturnToSpot();
         }
         else
         {
            if (!isDistracted)
            {
                //Debug.Log("moving");
                EnemySight();
                EnemyMovement();
            }
            else
            {
                if (pause)
                    LookAround();
                else
                    anim.SetBool("Distracted", true);

            }
         }

     }

     //-------------------------Sight-------------------------------------
     public void LookAround()
     {
         pauseTimer -= Time.deltaTime;

         if (pauseTimer <= 0)
         {
             pause = false;
             anim.SetBool("Pause", false);
             moveTimer = SetTimer();
         }
     }

    //----------------------------Movement-------------------------------
    public void EnemyMovement()
    {
        var forward = transform.TransformDirection(Vector3.forward);
        //Movement & pausing
        if (pause)
        {
            LookAround();
        }
        else
        {
            //When the moveTimer reaches zero, or the raycast collides with with objects on certain layermasks/objects
            moveTimer -= Time.deltaTime;
            RaycastHit hit;

            Debug.DrawRay(transform.position, forward * proximityAwareness, Color.blue);

            //if encountering an obstacle turn away
            if (Physics.Raycast(transform.position, forward, out hit, proximityAwareness, mask) ||
                Physics.Raycast(transform.position, forward, out hit, proximityAwareness, mask1))
            {
                //Debug.Log("hit!");
                //change direction
                direction += turnDirection;
                transform.eulerAngles = new Vector3(0, direction, 0);
            }

            if (moveTimer <= 0)
            {
                pause = true;
                anim.SetBool("Pause", true);
                pauseTimer = SetTimer();
            }

            agent.Move(forward * speed * Time.deltaTime);
        }
    }

    void ReturnToSpot()
    {
    	if (backToSpot == false)
    	{
    		Debug.Log("turn");
    		direction = turnDirection;
        }
        //Debug.Log("returning");
        agent.SetDestination(spot.position);
    }
}
