using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject checkBox;
    [SerializeField] private GameObject numberIndex;
    [SerializeField] private GameObject alphabetIndex;
    
    
    /// <summary>
    /// Start method.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void Start()
    {
        Toggle toggle = checkBox.GetComponent<Toggle>();
        toggle.isOn = PlayerPrefs.GetInt("ToggleState", 0) == 1;
    }

    /// <summary>
    /// On again button click.
    /// </summary>
    public void OnAgainButtonClick()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (Enum.TryParse(sceneName, out SceneType sceneType))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("Invalid input!");
        }
    }
        
    /// <summary>
    /// On x button click.
    /// </summary>
    public void OnXButtonClick()
    {
        settingsPanel.SetActive(false);
        LevelManager.CurrentState = LevelState.Play;
        Time.timeScale = 1;
    }

    /// <summary>
    /// On toggle button click.
    /// </summary>
    public void OnToggleButtonClick()
    {
        Toggle toggle = checkBox.GetComponent<Toggle>();
        PlayerPrefs.SetInt("ToggleState", toggle.isOn ? 1 : 0);
        PlayerPrefs.Save(); 
    }
}
