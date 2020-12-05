using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StationaryEnemy : Enemy
{
    //this script creates simple Enemy AI where the enemy will be stationary until it detects player
    private bool isSitting;
    public Transform seat;
    public float returnSpeed;
    public float rotationSpeed;
    Quaternion rotation;
    bool rotateToSeat;

    public GameObject Icon;

    public StationaryEnemy(bool doesPatrol) : base(doesPatrol)
    {
        isSitting = true;
    }

    private void Start()
    {
        isSitting = true;
        rotateToSeat = false;
        rotation = transform.rotation;
        footstepSounds.volume = PlayerPrefs.GetFloat("soundVolume");
        //Debug.Log(rotation.eulerAngles.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause && !isSitting)
        {
            //Debug.Log("play footsteps");
            footstepSounds.gameObject.SetActive(true);
        }
        else
        {
            footstepSounds.gameObject.SetActive(false);
        }

        if (rotateToSeat)
        {
            //Debug.Log(rotation.eulerAngles.y);
            Quaternion targetRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //Debug.Log(transform.rotation.eulerAngles.y);

            float difference = transform.rotation.eulerAngles.y - rotation.eulerAngles.y;
            //Debug.Log(difference);
            if (difference <= 3.0f && difference >= -3.0f)
            {
                anim.ResetTrigger("GetsDistracted");
                anim.SetTrigger("Sit");
                isSitting = true;
                rotateToSeat = false;
            }
        }

        if (pauseTimer > 0 && pause)
        {
            EnemySight();
            //Debug.Log(pauseTimer);
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                Debug.Log("pause over");
                pause = false;
                anim.SetBool("Pause", false);
                pauseTimer = 0;
                backToSpot = true;
            }
        }
            

        if (chase)
        {
            //Debug.Log("chasing");
            isSitting = false;
            DetectObstacles();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BasicMotions@Sprint01"))
                EnemyChase();

        }
        else if (isSitting == false && pause == false && !isDistracted)
        {
           //Debug.Log("Go to seat");
            DetectObstacles();               
            ReturnToSeat();
            EnemySight();
        }
        else
        {           

            if (!isDistracted)

            {
                //Debug.Log("normal");
                EnemySight();
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("NodOff")
                || anim.GetCurrentAnimatorStateInfo(1).IsName("male_emotion_laugh_on_chair")
                || anim.GetCurrentAnimatorStateInfo(1).IsName("male_talk_sit"))
                {
                    Icon.SetActive(false);
                }
                else
                    Icon.SetActive(true);
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("male_act_sit_on_chair"))
                {
                    agent.speed = 0;
                    anim.SetTrigger("GetsDistracted");
                    anim.SetBool("Distracted", true);
                    isSitting = false;
                }
                else
                    agent.speed = speed;                   

            }
                
        }
    }

    void ReturnToSeat()
    {
        if (backToSpot == false)
        {
            anim.ResetTrigger("Sit");            
            rotateToSeat = true;
        }
        
        if (isSitting == false)
        {
            agent.SetDestination(seat.position);
        }
        
    }

}
