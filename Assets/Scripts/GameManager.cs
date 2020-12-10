using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelCheckpoints;
    private int checkpointIndex;
    public SaveSystem saveSystem;
    public int levelNum;
    public static int prevLevel = 1;
    public static int lives = 3;
    public Text livesCounter;
    public AudioSource bgMusic;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        if (lives <= 0)
            lives = 3;

        livesCounter.text = "x" + lives;
        checkpointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointReached()
    {
        checkpointIndex++;
    }

    public void GoToCheckpoint(Transform player)
    {
        Vector3 position = levelCheckpoints[checkpointIndex].transform.position;
        player.position = position;
    }

    public void GoToChaseSequence()
    {
        //Load chase sequence scene
        SceneManager.LoadScene("ChaseSequence");
        prevLevel = levelNum;
        Debug.Log(prevLevel);

    }

    public void EndChase()
    {   
        saveSystem.CompleteLevel(prevLevel);
        if (prevLevel == 0)
            SceneManager.LoadScene("Dialogue System");
        else if (prevLevel == 2)
            SceneManager.LoadScene("LevelMap");
    }

    public void LoseLife()
    {
        lives--;
        livesCounter.text = "x" + lives;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("LevelMap");
        }
            
    }

    public void ToCampgrounds()
    {
        SceneManager.LoadScene("Campgrounds");
    }
}
