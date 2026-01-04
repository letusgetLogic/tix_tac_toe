using Enums;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("Blinking Arrow Player Turn")]
    [SerializeField, Range(0, 1)]
    private float blinkingTime = 0.5f;

    [Header("Cooldown Player Turn")]
    [SerializeField, Range(0, 1)]
    private float cooldownTime = 1f;

    [HideInInspector] public bool PlayerIsTurn;
    [HideInInspector] public TurnStates CurrentPlayerTurn;

    private bool randomTurn;

    public readonly TurnStates DefaultFirstTurn = TurnStates.PlayerX;
    public readonly TurnStates BotTurn = TurnStates.PlayerX;

    private float blinkingTimeCount;
    private bool isBlinkingTimeCountingDown;

    private bool setCooldown { get; set; }
    private bool isCountingDown { get; set; }
    private float cooldownTimeCountdown { get; set; }

    public int Round = 0;
    [SerializeField] private int roundDistance = 3;

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
    /// Initialize values.
    /// </summary>
    public void Init()
    {
        PlayerIsTurn = false;

        setCooldown = false;
        isCountingDown = false;
        cooldownTimeCountdown = 0;

        randomTurn = false;

        if (randomTurn == false)
            CurrentPlayerTurn = DefaultFirstTurn;
        
        if (LevelManager.Instance.Testing) 
            InitializeLevelWhileTesting();

        if (GameManager.Instance.IsBotActive)
            SetBotTurn();

        Round = 1;
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (!LevelManager.Instance.GameOver)
        {
            PlayerTurnBlinkingArrow();
        }

        if (GameManager.Instance.IsBotActive)
        {
            PLayerTurnCooldown();
        }
    }

    /// <summary>
    /// Blinking arrow to display player's turn.
    /// </summary>
    private void PlayerTurnBlinkingArrow()
    {
        // Timer
        if (isBlinkingTimeCountingDown) blinkingTimeCount -= Time.deltaTime;
        else                            blinkingTimeCount += Time.deltaTime;

        // Timer switch count
        if (blinkingTimeCount <= 0)             isBlinkingTimeCountingDown = false;
        if (blinkingTimeCount >= blinkingTime)  isBlinkingTimeCountingDown = true;

        UIManager.Instance.SetTurnX(
            CurrentPlayerTurn == TurnStates.PlayerX && 
            isBlinkingTimeCountingDown);

        UIManager.Instance.SetTurnO(
            CurrentPlayerTurn == TurnStates.PlayerO && 
            isBlinkingTimeCountingDown);

        // --- Old Version ---
        //
        //if (CurrentPlayerTurn == TurnStates.PlayerX)
        //{
        //    UIManager.Instance.OnEnablePlayerXTurn();

        //    SpriteRenderer spriteRendererX =
        //        UIManager.Instance.PlayerXTurnGameObject.GetComponent<SpriteRenderer>();

        //    // Blinking Arrow
        //    if (isBlinkingTimeCountingDown) spriteRendererX.enabled = true;
        //    else spriteRendererX.enabled = false;
        //}
        //else if (CurrentPlayerTurn == TurnStates.PlayerO)
        //{
        //    UIManager.Instance.OnEnablePlayerOTurn();

        //    SpriteRenderer spriteRendererO =
        //        UIManager.Instance.PlayerOTurnGameObject.GetComponent<SpriteRenderer>();

        //    // Blinking Arrow
        //    if (isBlinkingTimeCountingDown) spriteRendererO.enabled = true;
        //    else spriteRendererO.enabled = false;
        //}
    }

    /// <summary>
    /// Sets the player's turn.
    /// </summary>
    public void SetPlayerTurn()
    {
        PlayerIsTurn = false;

        SwitchTurn();

        if (randomTurn == false && CurrentPlayerTurn == DefaultFirstTurn)
        {
            Round++;

            if (GameManager.Instance.IsScalingUpActive)
            {
                Scoreboard.Instance.ScaleUpBase();
            }
        }

        AnimationManager.Instance.IsTurnEnding = false;
    }

    /// <summary>
    /// Bot's turn should delay after the player's turn, otherwise it comes directly after the player's turn.
    /// </summary>
    private void PLayerTurnCooldown()
    {
        if (setCooldown)
        {
            cooldownTimeCountdown = cooldownTime;
            isCountingDown = true;
            setCooldown = false;
        }

        if (isCountingDown)
        {
            if (cooldownTimeCountdown > 0f)
            {
                cooldownTimeCountdown -= Time.deltaTime;
            }
            else
            {
                Bot.Instance.OnRandomField();
                isCountingDown = false;
            }
        }
    }

    /// <summary>
    /// Is yet the scale up round?
    /// </summary>
    /// <returns></returns>
    public bool IsScalingUpRound()
    {
        if (Round <= roundDistance) return false;

        return (Round - 1) % roundDistance == 0;
    }

    private void SwitchTurn()
    {
        if (CurrentPlayerTurn == TurnStates.PlayerX)
        {
            CurrentPlayerTurn = TurnStates.PlayerO;

        }
        else if (CurrentPlayerTurn == TurnStates.PlayerO)
        {
            CurrentPlayerTurn = TurnStates.PlayerX;
        }
        if (GameManager.Instance.IsBotActive && CurrentPlayerTurn == BotTurn)
        {
            setCooldown = true;
        }
    }

    /// <summary>
    /// Initializes serialized score in inspector and runs the level.
    /// </summary>
    private void InitializeLevelWhileTesting()
    {
        string sceneType = SceneManager.GetActiveScene().name;

        if (Enum.TryParse(sceneType, out SceneType scene))
        {
            GameManager.LevelMode = scene;
        }
        else
        {
            Debug.Log("LevelManager::InitializeLevelWhileTesting: Invalid scene.!");
        }

        switch (GameManager.LevelMode)
        {
            case SceneType.LevelBotClassicScene:
                GameManager.Instance.IsClickingActive = true;
                GameManager.Instance.IsBotActive = true;
                return;

            case SceneType.LevelBotScalingUpScene:
                GameManager.Instance.IsClickingActive = true;
                GameManager.Instance.IsBotActive = true;
                GameManager.Instance.IsScalingUpActive = true;
                return;

            case SceneType.LevelDuelClassicScene:
                GameManager.Instance.IsClickingActive = true;
                return;

            case SceneType.LevelDuelScalingUpScene:
                GameManager.Instance.IsClickingActive = true;
                GameManager.Instance.IsScalingUpActive = true;
                return;

            case SceneType.LevelDuelBlockingScene:
                GameManager.Instance.IsBlockingActive = true;
                return;
        }
    }

    public void SetBotTurn()
    {
        setCooldown = true;
    }

}