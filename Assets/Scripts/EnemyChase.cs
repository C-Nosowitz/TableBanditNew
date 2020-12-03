using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.LoseLife();
            Debug.Log(GameManager.lives);
            
            if (GameManager.lives != 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
