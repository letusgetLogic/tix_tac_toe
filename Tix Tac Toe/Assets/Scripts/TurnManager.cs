using Enums;
using UnityEngine;

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

    private float blinkingTimeCount;
    private bool isBlinkingTimeCountingDown;

    private bool setCooldown;
    public bool StartCooldown;
    private float cooldownTimeCountdown;

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
    /// Start method.
    /// </summary>
    private void Start()
    {
        randomTurn = false;

        if (randomTurn == false)
        {
            CurrentPlayerTurn = TurnStates.PlayerX;
            UIManager.Instance.OnEnablePlayerXTurn();
        }

        setCooldown = false;
        StartCooldown = false;
        cooldownTimeCountdown = 0;
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
        else
        {
            CurrentPlayerTurn = TurnStates.PlayerEmpty;
            UIManager.Instance.OnDisableTurnSprites();
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
        else blinkingTimeCount += Time.deltaTime;

        // Timer switch count
        if (blinkingTimeCount <= 0) isBlinkingTimeCountingDown = false;
        if (blinkingTimeCount >= blinkingTime) isBlinkingTimeCountingDown = true;

        if (CurrentPlayerTurn == TurnStates.PlayerX)
        {
            UIManager.Instance.OnEnablePlayerXTurn();

            SpriteRenderer spriteRendererX =
                UIManager.Instance.PlayerXTurnGameObject.GetComponent<SpriteRenderer>();

            // Blinking Arrow
            if (isBlinkingTimeCountingDown) spriteRendererX.enabled = true;
            else spriteRendererX.enabled = false;
        }
        else if (CurrentPlayerTurn == TurnStates.PlayerO)
        {
            UIManager.Instance.OnEnablePlayerOTurn();

            SpriteRenderer spriteRendererO =
                UIManager.Instance.PlayerOTurnGameObject.GetComponent<SpriteRenderer>();

            // Blinking Arrow
            if (isBlinkingTimeCountingDown) spriteRendererO.enabled = true;
            else spriteRendererO.enabled = false;
        }
    }

    /// <summary>
    /// Sets the player's turn.
    /// </summary>
    public void SetPlayerTurn()
    {
        PlayerIsTurn = false;

        if (CurrentPlayerTurn == TurnStates.PlayerX)
        {
            CurrentPlayerTurn = TurnStates.PlayerO;

            if (GameManager.Instance.IsBotActive)
            {
                setCooldown = true;
            }
        }
        else if (CurrentPlayerTurn == TurnStates.PlayerO)
        {
            CurrentPlayerTurn = TurnStates.PlayerX;

            if (randomTurn == false)
            {
                Round++;

                if (GameManager.Instance.IsScaleUpActive)
                {
                    Scoreboard.Instance.ScaleUpBasis();
                }
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
            StartCooldown = true;
            setCooldown = false;
        }

        if (StartCooldown)
        {
            if (cooldownTimeCountdown > 0f)
            {
                cooldownTimeCountdown -= Time.deltaTime;
            }
            else
            {
                cooldownTimeCountdown = 0f;
                Bot.Instance.OnRandomField();
            }
        }
    }

    /// <summary>
    /// Is yet the scale up round?
    /// </summary>
    /// <returns></returns>
    public bool IsScaleUpRound()
    {
        if (Round <= roundDistance) return false;

        return (Round - 1) % roundDistance == 0;
    }
}