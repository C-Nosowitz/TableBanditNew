using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Allows for Saving/Loading (Input/Output)

public class SaveSystem : MonoBehaviour
{
    public static int[] levelsCompleted; //array of the levels, each index refers to a level --level[0] = level 1-- if set to 0 then level is not complete/set to 1 is complete
    
    void Awake()
    {
        //if save file does not yet exist
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            Debug.Log("file found");
            //Load();
        }
        else
        {
            Debug.Log("file not found");
            levelsCompleted = new int[5];
            for (int i = 0; i < levelsCompleted.Length; i++)
            {
                levelsCompleted[i] = 0;
            }
            Save();
        }

    }

    private void Save()
    {
        Debug.Log("saving");
        GameState game = new GameState
        {
            levelsCompleted = levelsCompleted,
        };

        string json = JsonUtility.ToJson(game);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void Load()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");   //Actually reads the text from a file.

        GameState game = JsonUtility.FromJson<GameState>(saveString);

        levelsCompleted = game.levelsCompleted;
    }

    public void CompleteLevel(int index)
    {
        Debug.Log(index);
        levelsCompleted[index] = 1;
        Save();
    }

    public bool CheckLevelIsComplete(int index)
    {
        if (levelsCompleted[index] == 1)
            return true;

        return false;
    }

    private class GameState
    {
        public int[] levelsCompleted;
    }
}
