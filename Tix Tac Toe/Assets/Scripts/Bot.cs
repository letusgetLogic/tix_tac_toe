using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : MonoBehaviour
{
    public static Bot Instance;

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
        InitializesArrayLength();
    }

    /// <summary>
    /// Generates random indexes of a field.
    /// </summary>
    public void OnRandomField()
    {
        List<GameObject> fieldList = new List<GameObject>();
        
        for (int horizontalIndex = 0; horizontalIndex < horizontalArrayLength; horizontalIndex++)
        {
            for (int verticalIndex = 0; verticalIndex < verticalArrayLength; verticalIndex++)
            {
                GameObject fieldGameObject = LevelManager.Instance.FieldArray[horizontalIndex, verticalIndex];
                
                Field field = fieldGameObject.GetComponent<Field>();
            
                if (field.State == FieldStates.FigureEmpty)
                {
                    fieldList.Add(fieldGameObject);
                }
            }
        }
        
        if (fieldList.Count > 0)  
        {
            int randomIndex = Random.Range(0, fieldList.Count);
            
            Field field = fieldList[randomIndex].GetComponent<Field>();
            field.ActivateFieldBehaviour();
        }

        TurnManager.Instance.StartCooldown = false;
    }
    
    /// <summary>
    /// Initializes the array's length.
    /// </summary>
    private void InitializesArrayLength()
    {
        horizontalArrayLength = LevelManager.Instance.NumberFieldHorizontal;
        verticalArrayLength = LevelManager.Instance.NumberFieldVertical;
    }
}
