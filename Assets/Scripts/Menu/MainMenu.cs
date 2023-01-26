using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, optionsMenu, newGameWarning;

    [SerializeField] Slider sfxSlider, musicSlider;

    //buttons
    public void NewGame()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.CURRENT_GAME_LEVEL) > 1 && !newGameWarning.activeInHierarchy)
        {
            NewGameWarning();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    public void Options()
    {
        if(mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            GetOptionsPlayerPrefs();
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.SFX_VOLUME, sfxSlider.value);
    }
    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.MUSIC_VOLUME, musicSlider.value);
    }

    public void NewGameWarning()
    {
        if (mainMenu.activeInHierarchy)
        {
            mainMenu.SetActive(false);
            newGameWarning.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            newGameWarning.SetActive(false);
        }
    }

    public void GetOptionsPlayerPrefs()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.SFX_VOLUME, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.MUSIC_VOLUME, 1f);
    }
}
