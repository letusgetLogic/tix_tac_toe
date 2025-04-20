using GlobalComponents;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float animationDelay = 1f; // Delay between 'field is clicked' and animation;
    [SerializeField] private Slider animationSlider;
    private readonly float[] sliderStates = { 0f, 0.5f, 1f };
        
    [Header("States")]
    // Booleans for the check methods at a certain time. It should run in order.
    public bool AnimationIsRunning;
       
    public bool Act1_IsRunning;
    public bool Act2_IsRunning;
    public bool Act3_IsRunning;
    public bool Act4_IsRunning;

    public bool IsCheckingFields;

    public bool SetAnimationDelay;
    private bool startsAnimationDelayCount;
    public float AnimationDelayCount; // Delay Count between 'field is clicked' and animation.
        
    public bool ScaleFieldsIsRunning;
    public bool ScaleFieldsHaveRunned;
    
    public bool SetScaleDuration;
    private bool startsScaleDuration;
    public float ScaleDurationCount;

    public bool IsTurnEnding;

    // Hides in inspector:
    
    private int[] savedFieldHorizontalIndex;
    private int[] savedFieldVerticalIndex;

    private int[] tempSavedFieldHorizontalIndex;
    private int[] tempSavedFieldVerticalIndex;

    private int indexHorizontalCount;
    private int indexVerticalCount;
    private int indexCount;
    private int indexCountTemp; // indexCount for temporary saved array.

    [HideInInspector] public Vector3 DefaultScale;

    private SliderDesignStates sliderDesignStates;

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
        AnimationIsRunning = false;
            
        Act1_IsRunning = false;
        Act2_IsRunning = false;
        Act3_IsRunning = false;
        Act4_IsRunning = false;

        IsCheckingFields = false;
        
        SetAnimationDelay = false;
        startsAnimationDelayCount = false;
        AnimationDelayCount = 0f;
        
        ScaleFieldsIsRunning = false;
        ScaleFieldsHaveRunned = false;

        SetScaleDuration = false;
        startsScaleDuration = false;
        ScaleDurationCount = 0f;

        IsTurnEnding = false;

        indexCount = 0;
        indexCountTemp = 0;

        sliderDesignStates = animationSlider.GetComponent<SliderDesignStates>();
        sliderDesignStates.GetSliderComponent(sliderStates, "AnimationDuration", animationDelay);
        sliderDesignStates.OnSliderValueChanged += HandleSliderChange;
    }

    /// <summary>
    /// On disable.
    /// </summary>
    private void OnDisable()
    {
        sliderDesignStates.OnSliderValueChanged -= HandleSliderChange;
    }

    /// <summary>
    /// Handle slider change.
    /// </summary>
    /// <param name="value"></param>
    private void HandleSliderChange(float value)
    {
        animationDelay = value;
    }
    
    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        if (AnimationIsRunning) ManageAnimation();
    }

    /// <summary>
    /// After field is clicked, runs this method.
    /// </summary>
    public void StartAnimation()
    {
        InitializeNewSavedArrays();
        AnimationIsRunning = true;
            
        Act1_IsRunning = true;
        IsCheckingFields = true;
    }

    /// <summary>
    /// Manages the animation runs.
    /// </summary>
    private void ManageAnimation()
    {
        // --- Checking Field's Section ---
        if (IsCheckingFields)
        {
            if (Act1_IsRunning) CheckScoreConditions.Instance.CheckDiagonal1();
            if (Act2_IsRunning) CheckScoreConditions.Instance.CheckColumn();
            if (Act3_IsRunning) CheckScoreConditions.Instance.CheckDiagonal2();
            if (Act4_IsRunning) CheckScoreConditions.Instance.CheckRow();
        }
            
        // Delay between 'field is clicked' and animation.
        if (SetAnimationDelay)
        {
            AnimationDelayCount = animationDelay;
            startsAnimationDelayCount = true;
            SetAnimationDelay = false;
        }

        if (startsAnimationDelayCount)
        {
            if (AnimationDelayCount > 0f)
            {
                AnimationDelayCount -= Time.deltaTime;
            }
            else
            {
                AnimationDelayCount = 0f;
                ScaleFieldsIsRunning = true;
                startsAnimationDelayCount = false;
            }
        }

        // --- Scale's Section ---
        if (ScaleFieldsIsRunning) // If the checked fields are enough for the scored points, runs the field's scale.
        {
            ScalesFields();

            if (Act1_IsRunning)
            {
                Act1_IsRunning = false;
                Act2_IsRunning = true;
                IsCheckingFields = true;
                return;
            }

            if (Act2_IsRunning)
            {
                Act2_IsRunning = false;
                Act3_IsRunning = true;
                IsCheckingFields = true;
                return;
            }

            if (Act3_IsRunning)
            {
                Act3_IsRunning = false;
                Act4_IsRunning = true;
                IsCheckingFields = true;
                return;
            }

            if (Act4_IsRunning)
            {
                SetScaleDuration = true;
                ScaleFieldsIsRunning = false;
            }
        }

        // --- Default Scale && Animation's Ending Section ---
        if (SetScaleDuration)
        {
            ScaleDurationCount = animationDelay;
            startsScaleDuration = true;
            SetScaleDuration = false;
        }

        if (startsScaleDuration)
        {
            if (LevelManager.Instance.FeatureNonstopScoreWhileScale) Scoreboard.Instance.CalculateScore();
            
            if (ScaleDurationCount > 0f)
            {
                ScaleDurationCount -= Time.deltaTime;
                Debug.Log(ScaleDurationCount);
            }
            else
            {
                ScaleDurationCount = 0f;

                IsTurnEnding = true;
                    
                ScalesDefault();
                    
                startsScaleDuration = false;
            }
        }

        if (IsTurnEnding)
        {
            LevelManager.Instance.EndTurn();
        }
    }

    /// <summary>
    /// Scales default value.
    /// </summary>
    private void ScalesDefault()
    {
        // Set field's scale at the maxScale.
        for (int i = 0; i <= indexCount; i++)
        {
            Field currentField = LevelManager.Instance.FieldArray[savedFieldHorizontalIndex[i],
                savedFieldVerticalIndex[i]].GetComponent<Field>();

            Transform spritesTransform = currentField.SpriteComponents.transform;

            spritesTransform.localScale = DefaultScale;
        }
    }

    /// <summary>
    /// Scaled fields animation.
    /// </summary>
    private void ScalesFields()
    {
        // Set field's scale at the maxScale.
        for (int i = 0; i <= indexCount; i++)
        {
            Field currentField = LevelManager.Instance.FieldArray[savedFieldHorizontalIndex[i],
                savedFieldVerticalIndex[i]].GetComponent<Field>();

            Transform spritesTransform = currentField.SpriteComponents.transform;

            spritesTransform.localScale = Vector3.one * maxScale;
                
            AudioManager.Instance.PlaySoundPoint();
        }
        
        CheckScoreConditions.Instance.CalculateMultiplier();
        
        if (Act4_IsRunning)
        {
            Scoreboard.Instance.CalculateScore();
        }
        
        ScaleFieldsIsRunning = false;
        ScaleFieldsHaveRunned = true;
    }

    /// <summary>
    /// Initializes the new temporary array.
    /// </summary>
    public void InitializeNewTempArrays()
    {
        tempSavedFieldHorizontalIndex = new int[CheckScoreConditions.Instance.LongestArrayLength];
        tempSavedFieldVerticalIndex = new int[CheckScoreConditions.Instance.LongestArrayLength];

        // Sets all index to -1, to check any value in there.
        for (int i = 0; i < tempSavedFieldHorizontalIndex.Length; i++)
        {
            tempSavedFieldHorizontalIndex[i] = -1;
        }

        for (int i = 0; i < tempSavedFieldVerticalIndex.Length; i++)
        {
            tempSavedFieldVerticalIndex[i] = -1;
        }

        indexCountTemp = 0;
    }

    /// <summary>
    /// Initializes the new saved array.
    /// </summary>
    public void InitializeNewSavedArrays()
    {
        int maxIndex = LevelManager.Instance.NumberFieldHorizontal * LevelManager.Instance.NumberFieldVertical;
        savedFieldHorizontalIndex = new int[maxIndex];
        savedFieldVerticalIndex = new int[maxIndex];

        // Sets all index to -1, to check any value in there.
        for (int i = 0; i < savedFieldHorizontalIndex.Length; i++)
        {
            savedFieldHorizontalIndex[i] = -1;
        }

        for (int i = 0; i < savedFieldVerticalIndex.Length; i++)
        {
            savedFieldVerticalIndex[i] = -1;
        }

        savedFieldHorizontalIndex[0] = CheckScoreConditions.Instance.IndexHorizontalOrigin;
        savedFieldVerticalIndex[0] = CheckScoreConditions.Instance.IndexVerticalOrigin;
    }

    /// <summary>
    /// Saves temporary checked fields in the additional arrays.
    /// </summary>
    /// <param name="indexHorizontalOrigin"></param>
    /// <param name="indexVerticalOrigin"></param>
    public void InitializeTempArrays(int indexHorizontalOrigin, int indexVerticalOrigin)
    {
        while (indexCountTemp < tempSavedFieldHorizontalIndex.Length)
        {
            if (tempSavedFieldHorizontalIndex[indexCountTemp] == -1) // If the index was not set, sets the values.
            {
                tempSavedFieldHorizontalIndex[indexCountTemp] = indexHorizontalOrigin;
                tempSavedFieldVerticalIndex[indexCountTemp] = indexVerticalOrigin;
                break;
            }

            indexCountTemp++;
        }
    }

    /// <summary>
    /// Initializes save field's array.
    /// </summary>
    public void InitializeSavedArrays()
    {
        // Runs into temporary array of saved fields.
        for (int i = 0; i <= indexCountTemp; i++)
        {
            // Runs into generally array of saved fields.
            for (indexCount = 0; indexCount < savedFieldHorizontalIndex.Length; indexCount++)
            {
                if (savedFieldHorizontalIndex[indexCount] == -1) // If the index was not set, sets the values.
                {
                    savedFieldHorizontalIndex[indexCount] = tempSavedFieldHorizontalIndex[i];
                    savedFieldVerticalIndex[indexCount] = tempSavedFieldVerticalIndex[i];

                    break;
                }
            }
        }
    }
}