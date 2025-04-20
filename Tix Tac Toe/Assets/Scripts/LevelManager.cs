using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
 
    public static LevelState CurrentState;
    
    [SerializeField] private GameObject fieldPrefab; 
    
    public int MinFieldsForScore = 3; // Minimum fields on the line for the scored points.
        
    public readonly int NumberFieldVertical = 9;
    public readonly int NumberFieldHorizontal = 9;
    
    public GameObject[,] FieldArray;
    
    [HideInInspector] public bool GameOver;
        
    public bool Testing = true;

    public bool FeatureNonstopScoreWhileScale = false;
        
    
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
        
        Time.timeScale = 1;
    }
        
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (Testing) InitializeLevelWhileTesting();
        
        CurrentState = LevelState.Play;
        GameOver = false;
        TurnManager.Instance.PlayerIsTurn = false;
        
        FieldArray = new GameObject[NumberFieldHorizontal, NumberFieldVertical];
        
        CreateFields();
    }
        
    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (GameOver) 
            foreach (GameObject field in FieldArray)
            {
                field.GetComponent<Field>().enabled = false;
            }
    }
        
    /// <summary>
    /// Instantiates fields on the playground.
    /// </summary>
    private void CreateFields()
    {
        for (int fieldIndexVertical = 0; fieldIndexVertical < FieldArray.GetLength(0); fieldIndexVertical++)
        {
            for (int fieldIndexHorizontal = 0; 
                 fieldIndexHorizontal < FieldArray.GetLength(1); fieldIndexHorizontal++)
            {
                Vector3 spawnPointPos = new Vector3(fieldIndexHorizontal, fieldIndexVertical, 0);
                    
                FieldArray[fieldIndexHorizontal, fieldIndexVertical] = 
                    Instantiate(fieldPrefab, spawnPointPos, Quaternion.identity);

                Field field = FieldArray[fieldIndexHorizontal, fieldIndexVertical].GetComponent<Field>();

                field.InitializeData(fieldIndexVertical, fieldIndexHorizontal, FieldStates.FigureEmpty);
            }
        }
        
        AnimationManager.Instance.DefaultScale = FieldArray[0, 0].GetComponent<Field>().transform.localScale;
    }

    /// <summary>
    /// Sets all default variables and switches turn. 
    /// </summary>
    public void EndTurn()
    {
        CheckScoreConditions.Instance.SetDefaultFieldCount();

        UIManager.Instance.TurnOffScoredPointsX();
        UIManager.Instance.TurnOffScoredPointsO();
        
        Scoreboard.Instance.SetDefaultMultiplier();
        Scoreboard.Instance.CheckResult();
        
        AnimationManager.Instance.ScaleFieldsHaveRunned = false;
        AnimationManager.Instance.Act4_IsRunning = false;
        AnimationManager.Instance.AnimationIsRunning = false;
        
        TurnManager.Instance.SetPlayerTurn();
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
            case SceneType.LevelBotEasyScene:
                GameManager.Instance.IsClickActive = true;
                GameManager.Instance.IsBotActive = true;
                return;
                    
            case SceneType.LevelDuelScene:
                GameManager.Instance.IsClickActive = true;
                return;
                    
            case SceneType.LevelDuelBlockScene:
                GameManager.Instance.IsBlockActive = true;
                return;
        }
    }
}