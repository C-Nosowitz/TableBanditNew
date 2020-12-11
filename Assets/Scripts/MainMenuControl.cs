using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider soundVolume;
    public Text soundValueText;
    public Slider musicVolume;
    public Text musicValueText;
    public AudioSource bgMusic;

    public GameObject credits;

    private void Start()
    {
        credits.SetActive(false);
    }

    private void Update()
    {
        bgMusic.volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }

    public void UpdateSlider()
    {
        PlayerPrefs.SetFloat("soundVolume", soundVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        soundValueText.text = "" + PlayerPrefs.GetFloat("soundVolume", 1);
        musicValueText.text = "" + PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }

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
        optionsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void HideCredits()
    {
        credits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
