using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GameObject[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        saveSystem.Load();
        for (int i = 1; i < levelButtons.Length; i++)
        {
            //if previous level is complete then make the next level button interactable
            if (saveSystem.CheckLevelIsComplete(i-1))
                levelButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
