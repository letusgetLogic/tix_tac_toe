using UnityEngine;
using Enums;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static SceneType LevelMode;

    public bool IsBotActive;
    public bool IsClickingActive;
    public bool IsBlockingActive;
    public bool IsScalingUpActive;
    
    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Object remains after scene change.
        }
        else
        {
            Destroy(gameObject); // Destroy the new instance instead of the old one.
        }
    }
}
