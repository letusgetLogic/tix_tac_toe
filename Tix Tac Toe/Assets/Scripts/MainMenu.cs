using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
        
    [SerializeField] private TMP_InputField playerScoreInput;
    [HideInInspector] public int InputScore;
        
    private readonly int maxInputScore = 99999;
        
        
    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject); 
        }

        Instance = this;
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        InputScore = -1;
        
        GameManager.Instance.IsBotActive = false;
        GameManager.Instance.IsClickActive = false;
        GameManager.Instance.IsBlockActive = false;
    }

    /// <summary>
    /// Starts the scene.
    /// </summary>
    public void OnPlayButtonClick()
    {
        string input = playerScoreInput.text;
            
        if (int.TryParse(input, out InputScore))
        {
            if (InputScore >= 0 && InputScore <= maxInputScore)
            {
                switch (GameManager.LevelMode)
                {
                    case SceneType.LevelBotEasyScene:
                        SceneManager.LoadScene(SceneType.LevelBotEasyScene.ToString());
                        GameManager.Instance.IsClickActive = true;
                        GameManager.Instance.IsBotActive = true;
                        return;
                    
                    case SceneType.LevelDuelScene:
                        SceneManager.LoadScene(SceneType.LevelDuelScene.ToString());
                        GameManager.Instance.IsClickActive = true;
                        return;
                    
                    case SceneType.LevelDuelBlockScene:
                        SceneManager.LoadScene(SceneType.LevelDuelBlockScene.ToString());
                        GameManager.Instance.IsBlockActive = true;
                        return;
                }
            }
        }
        else Debug.Log("Player score input is wrong!");
    }

    /// <summary>
    /// Click on mode bot easy.
    /// </summary>
    public void OnModeBotEasy()
    {
        GameManager.LevelMode = SceneType.LevelBotEasyScene;
    }

    /// <summary>
    /// Click on mode duel.
    /// </summary>
    public void OnModeDuel()
    {
        GameManager.LevelMode = SceneType.LevelDuelScene;
    }

    /// <summary>
    /// Click on mode block duel.
    /// </summary>
    public void OnModeDuelBlock()
    {
        GameManager.LevelMode = SceneType.LevelDuelBlockScene;
    }

    /// <summary>
    /// Closes the game.
    /// </summary>
    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
    }
}