using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BanditsMovement : MonoBehaviour
{
    public InventoryObj inventory;
    private int counter = 0;
    public Text inventoryCounter;
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnTime = 0.1f;
    float turnVel;
    //public Transform checkpoint;
    public GameManager gameManager;
    public float gravity;
    public float respawnTime = 2f;
    Vector3 moveDir;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator anim;

    public AudioSource eatEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if bandit has been captured
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Restart"))
        {
            
            if (respawnTime == 2)
                gameManager.GoToCheckpoint(transform);

            respawnTime -= Time.deltaTime;

            if (respawnTime <= 0)
            {
                anim.SetTrigger("Restart");
                transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = true;
                respawnTime = 2;
            }
                
        }
        else if (transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            

            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool("Moving", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVel, turnTime);//smooth numbers at angles
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (!OnGround())
                {
                    //Debug.Log("not grounded");
                    moveDir.y += gravity;
                }
                else
                    moveDir.y = 0;
                    

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
                anim.SetBool("Moving", false);

        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    bool OnGround()
    {
        //checking to see if player is on the ground to jump and move around
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("OnGround", isGrounded);
        return isGrounded;
    }

    void OnTriggerEnter(Collider collider)
    {
        var Item = collider.GetComponent<ItemClass>();
        if (Item)
        {
            anim.SetTrigger("Eat");
            eatEffect.Play();
            inventory.AddItem(Item.item, 1);
            counter++;
            inventoryCounter.text = "x" + counter;
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.CompareTag("Checkpoint"))
        {
            gameManager.CheckpointReached();
            collider.enabled = false;
            collider.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (collider.CompareTag("StartChase"))
        {
            Debug.Log("Chase start");
            var dialogue = collider.GetComponent<DialogueTrigger>();
            GetComponent<LookAhead>().levelEnd = true;
        }

        if (collider.CompareTag("EndChase"))
        {
            Debug.Log("Chase is over");
            gameManager.EndChase();
        }
    }

    public int GetInventoryCount()
    {
        return counter;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
﻿