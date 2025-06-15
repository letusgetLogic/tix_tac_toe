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

    public readonly int MaxInputScore = 999999999;

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
        GameManager.Instance.IsClickingActive = false;
        GameManager.Instance.IsBlockingActive = false;
        GameManager.Instance.IsScalingUpActive = false;
    }

    /// <summary>
    /// Starts the scene.
    /// </summary>
    public void OnPlayButtonClick()
    {
        string input = playerScoreInput.text;
            
        if (int.TryParse(input, out InputScore))
        {
            if (InputScore >= 0 && InputScore <= MaxInputScore)
            {
                switch (GameManager.LevelMode)
                {
                    case SceneType.LevelBotClassicScene:
                        SceneManager.LoadScene(SceneType.LevelBotClassicScene.ToString());
                        GameManager.Instance.IsClickingActive = true;
                        GameManager.Instance.IsBotActive = true;
                        return;

                    case SceneType.LevelDuelClassicScene:
                        SceneManager.LoadScene(SceneType.LevelDuelClassicScene.ToString());
                        GameManager.Instance.IsClickingActive = true;
                        return;

                    case SceneType.LevelBotScalingUpScene:
                        SceneManager.LoadScene(SceneType.LevelBotScalingUpScene.ToString());
                        GameManager.Instance.IsClickingActive = true;
                        GameManager.Instance.IsBotActive = true;
                        GameManager.Instance.IsScalingUpActive = true;
                        return;
                        
                    case SceneType.LevelDuelScalingUpScene:
                        SceneManager.LoadScene(SceneType.LevelBotScalingUpScene.ToString());
                        GameManager.Instance.IsClickingActive = true;
                        GameManager.Instance.IsScalingUpActive = true;
                        return;
                    
                    case SceneType.LevelDuelBlockingScene:
                        SceneManager.LoadScene(SceneType.LevelDuelBlockingScene.ToString());
                        GameManager.Instance.IsBlockingActive = true;
                        return;

                }
            }
        }
        else Debug.Log("Player score input is wrong!");
    }

    /// <summary>
    /// Click on mode bot classic.
    /// </summary>
    public void OnModeBotClassic()
    {
        GameManager.LevelMode = SceneType.LevelBotClassicScene;
    }

    /// <summary>
    /// Click on mode duel classic.
    /// </summary>
    public void OnModeDuelClassic()
    {
        GameManager.LevelMode = SceneType.LevelDuelClassicScene;
    }
    
    /// <summary>
    /// Click on mode bot scale up.
    /// </summary>
    public void OnModeBotScaleUp()
    {
        GameManager.LevelMode = SceneType.LevelBotScalingUpScene;
    }

    /// <summary>
    /// Click on mode duel scale up.
    /// </summary>
    public void OnModeDuelScaleUp()
    {
        GameManager.LevelMode = SceneType.LevelDuelScalingUpScene;
    }

    /// <summary>
    /// Click on mode block duel.
    /// </summary>
    public void OnModeDuelBlock()
    {
        GameManager.LevelMode = SceneType.LevelDuelBlockingScene;
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