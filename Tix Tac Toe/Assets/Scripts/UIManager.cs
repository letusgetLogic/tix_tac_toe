using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
        
    [Header("Components")]
    [SerializeField] public GameObject PlayerXTurnGameObject;
    [SerializeField] public GameObject PlayerOTurnGameObject;
    [SerializeField] public GameObject ZickZackGameObject;
    [SerializeField] public GameObject ZickZack2GameObject;
    
    [SerializeField] private TextMeshProUGUI endPointsText;
    [SerializeField] private TextMeshProUGUI currentPointsXText;
    [SerializeField] private TextMeshProUGUI currentPointsOText;
    [SerializeField] private TextMeshProUGUI movesNumberXText;
    [SerializeField] private TextMeshProUGUI movesNumberOText;
    [SerializeField] private TextMeshProUGUI basisPointsXText;
    [SerializeField] private TextMeshProUGUI basisPointsOText;
    [SerializeField] private TextMeshProUGUI multiplierXText;
    [SerializeField] private TextMeshProUGUI multiplierOText;
    [SerializeField] private TextMeshProUGUI scoredPointsXText;
    [SerializeField] private TextMeshProUGUI scoredPointsOText;
    [SerializeField] private TextMeshProUGUI congratulationsText;
    [SerializeField] private GameObject scoredPointsXGameObject;
    [SerializeField] private GameObject scoredPointsOGameObject;
    [SerializeField] private GameObject settingsPanel;
   

    [HideInInspector] public int MovesPlayerX;
    [HideInInspector] public int MovesPlayerO;
        
    
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
        scoredPointsXText.enabled = false;
        scoredPointsOText.enabled = false;
            
        scoredPointsXGameObject.SetActive(false);
        scoredPointsOGameObject.SetActive(false);
            
        PlayerXTurnGameObject.SetActive(false);
        PlayerOTurnGameObject.SetActive(false);
            
        congratulationsText.enabled = false;

        MovesPlayerX = 0;
        MovesPlayerO = 0;
            
        ZickZackGameObject.SetActive(false);
        ZickZack2GameObject.SetActive(false);
        
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Initializes end points in UI.
    /// </summary>
    public void InitializeEndPoints()
    {
        endPointsText.text = Scoreboard.Instance.EndPoints.ToString();
    }

    /// <summary>
    /// Update Method.
    /// </summary>
    private void Update()
    {
        currentPointsXText.text = Scoreboard.Instance.ScoreX.ToString();
        currentPointsOText.text = Scoreboard.Instance.ScoreO.ToString();
        movesNumberXText.text = MovesPlayerX.ToString();
        movesNumberOText.text = MovesPlayerO.ToString();
        basisPointsXText.text = Scoreboard.Instance.BasisPointsX.ToString();
        basisPointsOText.text = Scoreboard.Instance.BasisPointsO.ToString();
        multiplierXText.text = Scoreboard.Instance.MultiplierPointsX.ToString();
        multiplierOText.text = Scoreboard.Instance.MultiplierPointsO.ToString();
        scoredPointsXText.text = Scoreboard.Instance.ScoredPointsX.ToString();
        scoredPointsOText.text = Scoreboard.Instance.ScoredPointsO.ToString();
    }
       
        
    /// <summary>
    /// On back button click.
    /// </summary>
    public void OnBackButtonClick()
    {
        SceneManager.LoadScene(SceneType.MainMenuScene.ToString());
    }
  
    /// <summary>
    /// On settings button click.
    /// </summary>
    public void OnSettingsButtonClick()
    {
        if (LevelManager.CurrentState == LevelState.Play)
        {
            settingsPanel.SetActive(true);
            settingsPanel.GetComponent<SettingsPanel>();
            LevelManager.CurrentState = LevelState.Pause;
            Time.timeScale = 0;
        }
        else if (LevelManager.CurrentState == LevelState.Pause)
        {
            settingsPanel.SetActive(false);
            LevelManager.CurrentState = LevelState.Play;
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Activates the scored points for player X.
    /// </summary>
    public void TurnOnScoredPointsX()
    {
        scoredPointsXText.enabled = true;
        scoredPointsXGameObject.SetActive(true);
    } 
        
    /// <summary>
    /// Activates the scored points for player O.
    /// </summary>
    public void TurnOnScoredPointsO()
    {
        scoredPointsOText.enabled = true;
        scoredPointsOGameObject.SetActive(true);
    } 
    
    /// <summary>
    /// Deactivates the scored points for player X.
    /// </summary>
    public void TurnOffScoredPointsX()
    {
        scoredPointsXText.enabled = false;
        scoredPointsXGameObject.SetActive(false);
    }
    
    /// <summary>
    /// Deactivates the scored points for player O.
    /// </summary>
    public void TurnOffScoredPointsO()
    {
        scoredPointsOText.enabled = false;
        scoredPointsOGameObject.SetActive(false);
    }

    /// <summary>
    /// Deactivates the turn arrows.
    /// </summary>
    public void OnDisableTurnSprites()
    {
        PlayerXTurnGameObject.SetActive(false);
        PlayerOTurnGameObject.SetActive(false);
    }

    /// <summary>
    /// Displays the text and the animation for the ending.
    /// </summary>
    public void OnEnableCongratulationsTextX()
    {
        congratulationsText.enabled = true;
        congratulationsText.text = "Player X wins!";
        ZickZackGameObject.SetActive(true);
        ZickZack2GameObject.SetActive(true);
    }
    
    /// <summary>
    /// Displays the text and the animation for the ending.
    /// </summary>
    public void OnEnableCongratulationsTextO()
    {
        congratulationsText.enabled = true;
        congratulationsText.text = "Player O wins!";
        ZickZackGameObject.SetActive(true);
        ZickZack2GameObject.SetActive(true);
    }
    
    /// <summary>
    /// Displays the text and the animation for the ending.
    /// </summary>
    public void OnEnableCongratulationsTextDraw()
    {
        congratulationsText.enabled = true;
        congratulationsText.text = "- Draw -";
    }

    /// <summary>
    /// Sets GameObject of PlayerXTurn active and of PlayerOTurn inactive.
    /// </summary>
    public void OnEnablePlayerXTurn()
    {
        PlayerXTurnGameObject.SetActive(true);
        PlayerOTurnGameObject.SetActive(false);
    }
        
    /// <summary>
    /// Sets GameObject of PlayerOTurn active and of PlayerXTurn inactive.
    /// </summary>
    public void OnEnablePlayerOTurn()
    {
        PlayerOTurnGameObject.SetActive(true);
        PlayerXTurnGameObject.SetActive(false);
    }
}