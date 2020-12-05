using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider soundVolume;
    public Slider musicVolume;

    public void Play()
    {
        SceneManager.LoadScene("LevelMap");
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
        soundVolume.value = PlayerPrefs.GetFloat("soundVolume", 1);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }

    public void ExitOptions()
    {
        PlayerPrefs.SetFloat("soundVolume", soundVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        optionsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
