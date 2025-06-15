using Enums;
using UnityEngine;

public class CheckScoreConditions : MonoBehaviour
{
    public static CheckScoreConditions Instance;

    [Header("--- NonSerialized ---")]
    // Field's Data.
    public int IndexHorizontalOrigin;
    public int IndexVerticalOrigin;
    public FieldStates FieldStateOrigin;
        
    // Variables for the counter in different directions.
    private int fieldCountDiagonal1;
    private int fieldCountColumn;
    private int fieldCountDiagonal2;
    private int fieldCountRow;

    // Variables for the abbreviation of 'LevelManager.Instance. and of 'FieldArray.GetLength()'.
    [HideInInspector] public int LongestArrayLength;
    private int horizontalArrayLength;
    private int verticalArrayLength;
        
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
        IndexHorizontalOrigin = -1;
        IndexVerticalOrigin = -1;
        FieldStateOrigin = FieldStates.FigureEmpty;
            
        InitializesArrayLength();
    }
        
    /// <summary>
    /// Initializes Data.
    /// </summary>
    /// <param name="fieldIndexVertical"></param>
    /// <param name="fieldIndexHorizontal"></param>
    /// <param name="fieldState"></param>
    public void InitializeData(int fieldIndexHorizontal, int fieldIndexVertical, FieldStates fieldState)
    {
        this.IndexHorizontalOrigin = fieldIndexHorizontal;
        this.IndexVerticalOrigin = fieldIndexVertical;
        this.FieldStateOrigin = fieldState;
    }
    
    /// <summary>
    /// Checks the fields on the diagonal right-slanted row.
    /// </summary>
    public void CheckDiagonal1()
    {
        // 1 is the clicked field.
        fieldCountDiagonal1 = 1 + TopLeftCheckedField() + BottomRightCheckedField();
            
        // Here are 2 conditions:
        // 1. If field count is not enough for score, go in AnimationManager.cs to the next act.
        // 2. If field count is enough for score, sets delay, then scales the fields,
        // afterward go in AnimationManager.cs to the next act / 'check fields' method.

        if (fieldCountDiagonal1 < LevelManager.Instance.MinFieldsForScore)
        {
            fieldCountDiagonal1 = 1;
                
            AnimationManager.Instance.Act1_IsRunning = false;
            AnimationManager.Instance.Act2_IsRunning = true;
        }
        else // Field count are enough for score.
        {
            AnimationManager.Instance.InitializeSavedArrays();
            AnimationManager.Instance.SetAnimationDelay = true;
            AnimationManager.Instance.IsCheckingFields = false;
        }
           
        AnimationManager.Instance.InitializeNewTempArrays();
    }
        
    /// <summary>
    ///  Checks the fields on the column.
    /// </summary>
    public void CheckColumn()
    {
        // 1 is the clicked field. 
        fieldCountColumn = 1 + TopCheckedField() + BottomCheckedField();
            
        // Here are 2 conditions:
        // 1. If field count is not enough for score, go in AnimationManager.cs to the next act.
        // 2. If field count is enough for score, sets delay, then scales the fields,
        // afterward go in AnimationManager.cs to the next act / 'check fields' method.
            
        if (fieldCountColumn < LevelManager.Instance.MinFieldsForScore) 
        {
            fieldCountColumn = 1;
                
            AnimationManager.Instance.Act2_IsRunning = false;
            AnimationManager.Instance.Act3_IsRunning = true;
        }
        else // Field count are enough for score.
        {
            AnimationManager.Instance.InitializeSavedArrays();
            AnimationManager.Instance.SetAnimationDelay = true;
            AnimationManager.Instance.IsCheckingFields = false;
        }
            
        AnimationManager.Instance.InitializeNewTempArrays();
    }

    /// <summary>
    /// Checks the fields on the diagonal left-slanted row.
    /// </summary>
    public void CheckDiagonal2()
    {
        // 1 is the clicked field. 
        fieldCountDiagonal2 = 1 + TopRightCheckedField() + BottomLeftCheckedField();
            
        // Here are 2 conditions:
        // 1. If field count is not enough for score, go in AnimationManager.cs to the next act.
        // 2. If field count is enough for score, sets delay, then scales the fields,
        // afterward go in AnimationManager.cs to the next act / 'check fields' method.

        if (fieldCountDiagonal2 < LevelManager.Instance.MinFieldsForScore)
        {
            fieldCountDiagonal2 = 1;
                
            AnimationManager.Instance.Act3_IsRunning = false;
            AnimationManager.Instance.Act4_IsRunning = true;
        }
        else // Field count are enough for score.
        {
            AnimationManager.Instance.InitializeSavedArrays();
            AnimationManager.Instance.SetAnimationDelay = true;
            AnimationManager.Instance.IsCheckingFields = false;
        }
            
        AnimationManager.Instance.InitializeNewTempArrays();
    }

    /// <summary>
    /// Checks the fields on the row.
    /// </summary>
    public void CheckRow()
    {
        // 1 is the clicked field.
        fieldCountRow = 1 + LeftCheckedField() + RightCheckedField();
            
        // Here are 3 conditions:
        // 1. If field count is not enough for score, but the first acts are enough for score, animates score,
        // 2. otherwise ends the animation methods and sets turn.
        // 3. If field count is enough for score, sets delay, then scales the fields,
        // afterward ends the animation methods and sets turn.

        if (fieldCountRow < LevelManager.Instance.MinFieldsForScore)
        {
            fieldCountRow = 1;
                
            // In the case at least 1 of the first 3 acts have scaled but act 4 hasn't.
            if (AnimationManager.Instance.ScaleFieldsHaveRunned)
            {
                Scoreboard.Instance.CalculateScore();
                AnimationManager.Instance.SetScaleDuration = true;
            }
            else // No acts have enough field count for score.
            {
                LevelManager.Instance.EndTurn();
            }
        }
        else // Field count are enough for score.
        {
            AnimationManager.Instance.InitializeSavedArrays();
            AnimationManager.Instance.SetAnimationDelay = true; // Starts the score animation.
        }
            
        AnimationManager.Instance.IsCheckingFields = false;
        AnimationManager.Instance.InitializeNewTempArrays();
    }
        
    /// <summary>
    /// Checked field go to the top left of current field.
    /// </summary>
    private int TopLeftCheckedField()
    {
        int countDiagonal1 = 0;
            
        for (int checkDistanceIndex = 1; checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin - checkDistanceIndex;
            int indexVertical = IndexVerticalOrigin + checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal >= 0 && indexVertical < verticalArrayLength)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countDiagonal1++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countDiagonal1;
    }
        
    /// <summary>
    /// Checked field go to the bottom right of current field.
    /// </summary>
    private int BottomRightCheckedField()
    {
        int countDiagonal1 = 0;
            
        for (int checkDistanceIndex = 1; checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin + checkDistanceIndex;
            int indexVertical = IndexVerticalOrigin - checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal < horizontalArrayLength && indexVertical >= 0)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countDiagonal1++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countDiagonal1;
    }
        
    /// <summary>
    /// Checked field go to the top of current field.
    /// </summary>
    private int TopCheckedField()
    {
        int countColumn = 0;
            
        for (int checkDistanceIndex = 1; checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexVertical = IndexVerticalOrigin + checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexVertical < verticalArrayLength)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[IndexHorizontalOrigin, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countColumn++;
                    AnimationManager.Instance.InitializeTempArrays(IndexHorizontalOrigin, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countColumn;
    }
        
    /// <summary>
    /// Checked field go to the bottom of current field.
    /// </summary>
    private int BottomCheckedField()
    {
        int countColumn = 0;
            
        for (int checkDistanceIndex = 1; checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexVertical = IndexVerticalOrigin - checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexVertical >= 0)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[IndexHorizontalOrigin, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countColumn++;
                    AnimationManager.Instance.InitializeTempArrays(IndexHorizontalOrigin, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countColumn;
    }
        
    /// <summary>
    /// Checked field go to the top right of current field.
    /// </summary>
    private int TopRightCheckedField()
    {
        int countDiagonal2 = 0;
            
        for (int checkDistanceIndex = 1; checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin + checkDistanceIndex;
            int indexVertical = IndexVerticalOrigin + checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal < horizontalArrayLength && indexVertical < verticalArrayLength)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countDiagonal2++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countDiagonal2;
    }
        
    /// <summary>
    /// Checked field go to the bottom left of current field.
    /// </summary>
    private int BottomLeftCheckedField()
    {
        int countDiagonal2 = 0;
            
        for (int checkDistanceIndex = 1;  checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin - checkDistanceIndex;
            int indexVertical = IndexVerticalOrigin - checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal >= 0 && indexVertical >= 0)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, indexVertical].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countDiagonal2++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, indexVertical);
                } 
                else break;
            } 
            else break;
        }
        return countDiagonal2;
    }
        
    /// <summary>
    /// Checked field go to the left of current field.
    /// </summary>
    private int LeftCheckedField()
    {
        int countRow = 0;
            
        for (int checkDistanceIndex = 1;  checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin - checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal >= 0)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, IndexVerticalOrigin].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countRow ++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, IndexVerticalOrigin);
                } 
                else break;
            } 
            else break;
        }
        return countRow ;
    }
        
    /// <summary>
    /// Checked field go to the right of current field.
    /// </summary>
    private int RightCheckedField()
    {
        int countRow = 0;
            
        for (int checkDistanceIndex = 1;  checkDistanceIndex < LongestArrayLength; checkDistanceIndex++)
        {
            int indexHorizontal = IndexHorizontalOrigin + checkDistanceIndex;
                
            // Runs only if current checked field is in the array's indexes.
            if (indexHorizontal < horizontalArrayLength)
            {
                Field checkedField = 
                    LevelManager.Instance.FieldArray[indexHorizontal, IndexVerticalOrigin].GetComponent<Field>();
                    
                if (checkedField.State == FieldStateOrigin)
                {
                    countRow ++;
                    AnimationManager.Instance.InitializeTempArrays(indexHorizontal, IndexVerticalOrigin);
                } 
                else break;
            } 
            else break;
        }
        return countRow ;
    }
    
    /// <summary>
    /// Calculates the multiplier.
    /// </summary>
    public void CalculateMultiplier()
    {
        int sumMultiplier = fieldCountDiagonal1 * fieldCountColumn * fieldCountDiagonal2 * fieldCountRow;
            
        if (sumMultiplier >= LevelManager.Instance.MinFieldsForScore)
            Scoreboard.Instance.ImplementMultiplier(sumMultiplier);
    }

    /// <summary>
    /// Initializes the array's length.
    /// </summary>
    private void InitializesArrayLength()
    {
        if (LevelManager.Instance.NumberFieldHorizontal > LevelManager.Instance.NumberFieldVertical)
        {
            LongestArrayLength = LevelManager.Instance.NumberFieldHorizontal;
        }
        else LongestArrayLength = LevelManager.Instance.NumberFieldVertical;
            
        horizontalArrayLength = LevelManager.Instance.NumberFieldHorizontal;
        verticalArrayLength = LevelManager.Instance.NumberFieldVertical;
    }

    /// <summary>
    /// Sets default field counters on each row.
    /// </summary>
    public void SetDefaultFieldCount()
    {
        fieldCountDiagonal1 = 1;
        fieldCountColumn = 1;
        fieldCountDiagonal2 = 1;
        fieldCountRow = 1;
    }
}