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

    public readonly Vector2Int CenterIndex = new Vector2Int(4, 4);

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
        CurrentState = LevelState.Play;
        GameOver = false;
        
        FieldArray = new GameObject[NumberFieldHorizontal, NumberFieldVertical];
        
        CreateFields();

        TurnManager.Instance.Init();
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
        for (int row = 0; row < FieldArray.GetLength(0); row++)
        {
            for (int col = 0; col < FieldArray.GetLength(1); col++)
            {
                Vector3 spawnPointPos = new Vector3(col, row, 0);
                    
                FieldArray[col, row] = 
                    Instantiate(fieldPrefab, spawnPointPos, Quaternion.identity);

                Field field = FieldArray[col, row].GetComponent<Field>();

                field.InitializeData(col, row, FieldStates.FigureEmpty);
            }
        }
        
        AnimationManager.Instance.DefaultScale = FieldArray[0, 0].GetComponent<Field>().transform.localScale;
    }

    /// <summary>
    /// Sets all default variables and switches turn. 
    /// </summary>
    public void EndTurn()
    {
        AnimationManager.Instance.ScaleFieldsHaveRunned = false;
        AnimationManager.Instance.Act4_IsRunning = false;
        AnimationManager.Instance.AnimationIsRunning = false;

        CheckScoreConditions.Instance.SetDefaultFieldCount();

        UIManager.Instance.TurnOffScoredPointsX();
        UIManager.Instance.TurnOffScoredPointsO();
        
        Scoreboard.Instance.SetDefaultMultiplier();

        if (Scoreboard.Instance.IsGameOver() == false)
            TurnManager.Instance.SetPlayerTurn();
    }

}