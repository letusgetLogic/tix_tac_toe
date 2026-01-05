using System;
using Enums;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Instance;

    [Header("========== Goal Score ==========")]
    
    [Header("--- NonSerialized ---")]
    public int EndPoints;
    
    [Header("--- SerializeField ---")]
    public int EndPointsTesting;
    public int BasePoints = 10;
    public int BasePointsScalingUp = 10;

    [Header("========== Player X ==========")]
    
    [Header("--- NonSerialized ---")]
    public int ScoreX;
    public int ScoredPointsX;
    public int MultiplierPointsX;
    
    [Header("========== Player O ==========")]
    
    [Header("--- NonSerialized ---")]
    public int ScoreO;
    public int ScoredPointsO;
    public int MultiplierPointsO;

   


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
        if (LevelManager.Instance.Testing)
        {
            EndPoints = EndPointsTesting;
            UIManager.Instance.InitializeEndPoints(EndPoints.ToString());
        }
        else InitializeScore();
    }

    /// <summary>
    /// Initializes the set score.
    /// </summary>
    private void InitializeScore()
    {
        switch (MainMenu.Instance.InputScore)
        {
            case 0:
                EndPoints = MainMenu.Instance.MaxInputScore;
                UIManager.Instance.InitializeEndPoints("\u221E");
                break;
            case > 0:
                EndPoints = MainMenu.Instance.InputScore;
                UIManager.Instance.InitializeEndPoints(EndPoints.ToString());
                break;
        }
    }
        
    /// <summary>
    /// Implements multiplier.
    /// </summary>
    /// <param name="multiplier"></param>
    public void ImplementMultiplier(int multiplier)
    {
        if (TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerX)
        {
            MultiplierPointsX = multiplier;
        }
        else if (TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerO)
        {
            MultiplierPointsO = multiplier;
        }
    }

    /// <summary>
    /// Sets the default multiplier.
    /// </summary>
    public void SetDefaultMultiplier()
    {
        MultiplierPointsX = 0;
        MultiplierPointsO = 0;
    }

    /// <summary>
    /// Calculates the score.
    /// </summary>
    public void CalculateScore()
    {
        ScoredPointsX = BasePoints * MultiplierPointsX;
        ScoreX += ScoredPointsX;
        ScoredPointsO = BasePoints * MultiplierPointsO;
        ScoreO += ScoredPointsO;
        
        if (ScoredPointsX > 0)
        {
            UIManager.Instance.TurnOnScoredPointsX();
        }
        else if (ScoredPointsO > 0)
        {
            UIManager.Instance.TurnOnScoredPointsO();
        }
    }
        
    /// <summary>
    /// Returns the win state.
    /// </summary>
    /// <returns></returns>
    public WinState CheckCurrentLevelStates()
    {
        if (UIManager.Instance.MovesPlayerX == UIManager.Instance.MovesPlayerO)
        {
            if (ScoreX >= EndPoints && ScoreX == ScoreO)
                return WinState.Draw;   
                
            if (ScoreX >= EndPoints && ScoreX > ScoreO)
                return WinState.WinnerPlayerX;
                
            if (ScoreO >= EndPoints && ScoreO > ScoreX)
                return WinState.WinnerPlayerO;

            int moves = UIManager.Instance.MovesPlayerX + UIManager.Instance.MovesPlayerO;
            Debug.Log("Moves " + moves + " / " + EvenFullFields());

            if (moves == EvenFullFields())
            {
                if (ScoreX > ScoreO)
                    return WinState.WinnerPlayerX;
                    
                if (ScoreO > ScoreX)
                    return WinState.WinnerPlayerO;
                    
                if (ScoreX == ScoreO)
                    return WinState.Draw;
            }
        }
        return WinState.None;
    }
        
    /// <summary>
    /// Checks the result.
    /// </summary>
    public bool IsGameOver()
    {
        switch (CheckCurrentLevelStates())
        {
            case WinState.WinnerPlayerX:
                LevelManager.Instance.GameOver = true;
                TurnManager.Instance.CurrentPlayerTurn = TurnStates.PlayerEmpty;
                UIManager.Instance.OnEnableCongratulationsTextX();
                return true;
                
            case WinState.WinnerPlayerO:
                LevelManager.Instance.GameOver = true;
                TurnManager.Instance.CurrentPlayerTurn = TurnStates.PlayerEmpty;
                UIManager.Instance.OnEnableCongratulationsTextO();
                return true;
                
            case WinState.Draw:
                LevelManager.Instance.GameOver = true;
                TurnManager.Instance.CurrentPlayerTurn = TurnStates.PlayerEmpty;
                UIManager.Instance.OnEnableCongratulationsTextDraw();
                return true;

            case WinState.None:
                return false;
        }

        return default;
    }

    /// <summary>
    /// Scales up the base points.
    /// </summary>
    public void ScaleUpBase()
    {
        if (TurnManager.Instance.IsScalingUpRound())
        {
            BasePoints += BasePointsScalingUp;
        }
    }
        
    /// <summary>
    /// Returns the even total amount of fields on the board.
    /// </summary>
    /// <returns></returns>
    private int EvenFullFields()
    {
        int fieldsSum = LevelManager.Instance.NumberFieldHorizontal * LevelManager.Instance.NumberFieldVertical;
        return (int)(fieldsSum * 0.5f) * 2;
    }
}