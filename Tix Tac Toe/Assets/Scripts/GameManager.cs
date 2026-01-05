using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static SceneType LevelMode;

    public bool IsBotActive { get; set; }
    public bool IsClickingActive { get; set; }
    public bool IsBlockingActive { get; set; }
    public bool IsScalingUpActive { get; set; }

    public float AnimationDelayDefault => animationDelayDefault;
    [SerializeField] private float animationDelayDefault = 1f; // Delay between 'field is clicked' and animation;

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

    private void Start()
    {
        PlayerPrefs.SetFloat("AnimationDuration", animationDelayDefault);
    }
}
