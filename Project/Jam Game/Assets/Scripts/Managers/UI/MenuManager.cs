using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class MenuManager : MonoBehaviour
{
    public GameObject settingPanel;

    public void OnSettingClicked()
    {
        settingPanel.SetActive(true);
    }

    public void OnResumeClicked() 
    {
        GameManager.Instance.ToggleGamePause();
    }

    public void OnExitClicked() 
    { 
        
    }

    public void OnStartClicked() { 
        SceneManager.LoadScene("MainGame");
    }

    public void MainMenuClicked() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetSFXVolumn(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetSFXVolume(value);
        else { 
            Settings.Instance.SFXVolumn = value;
        }
            
    }

    public void SetBGMVolumn(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(value);
        else
        {
            Settings.Instance.BGMVolumn = value;
        }
    }
}
