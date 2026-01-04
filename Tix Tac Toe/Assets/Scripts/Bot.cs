using System.Collections.Generic;
using Enums;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : MonoBehaviour
{
    public static Bot Instance;

    private int width => LevelManager.Instance.FieldArray.GetLength(1);
    private int height => LevelManager.Instance.FieldArray.GetLength(0);
    private Vector2Int centerIndex => LevelManager.Instance.CenterIndex;

    private readonly Vector2Int[] dir = new Vector2Int[]
    {
        new(-1, 1), new(0, 1), new(1, 1),
        new(-1, 0), new(0, 0), new(1, 0),
        new(-1, -1),new(0, -1), new(1, -1),
    };

    private bool isFirstTurn = true;

    private Queue<Field> lastFields = new Queue<Field>();

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
    /// Generates random indexes of a field.
    /// </summary>
    public void OnRandomField()
    {
        Debug.Log("Bot::OnRandomField: Bot is making a move.");
        var emptyFields = new List<Field>();

        if (isFirstTurn)
        {
            var (fields, hasEmpty) = SearchEmptyAround(centerIndex);
            emptyFields = fields;
            isFirstTurn = false;

        }
        else if (lastFields.Count > 0)
        {
            var (fields, hasEmpty) =
                SearchEmptyAround(new Vector2Int(lastFields.Peek().Col, lastFields.Peek().Row));
            if (hasEmpty)
            {
                emptyFields = fields;
            }
            else
            {
                Debug.Log("No empty fields around " + new Vector2Int(lastFields.Peek().Col, lastFields.Peek().Row));
                lastFields.Dequeue();
                OnRandomField();
                return;
            }
        }
        else
        {
            Debug.Log("No last fields");
            emptyFields = SearchEmptyAllOver();
        }

        TickFieldIn(emptyFields);
    }

    private void TickFieldIn(List<Field> emptyFields)
    {
        if (emptyFields.Count == 0)
            return;

        int rnd = Random.Range(0, emptyFields.Count);
        Field field = emptyFields[rnd].GetComponent<Field>();

        field.ActivateFieldBehaviour();
        lastFields.Enqueue(field);
    }

    private (List<Field>, bool) SearchEmptyAround(Vector2Int startPoint)
    {
        Debug.Log("Bot::SearchEmptyAround: Searching empty fields around " + startPoint);
        var fieldList = new List<Field>();
        bool hasEmpty = false;

        foreach (var direction in dir)
        {
            Vector2Int checkIndex = new Vector2Int(startPoint.x + direction.x, startPoint.y + direction.y);
            if (checkIndex.x >= 0 && checkIndex.x < width && checkIndex.y >= 0 && checkIndex.y < height)
            {
                bool wasAddingEmpty = AddEmptyToList(ref fieldList, checkIndex);

                if (!hasEmpty)
                    hasEmpty = wasAddingEmpty;
            }
        }
        return (fieldList, hasEmpty);
    }

    private List<Field> SearchEmptyAllOver()
    {
        Debug.Log("Bot::SearchEmptyAllOver: Searching empty fields all over the level.");
        var fieldList = new List<Field>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                AddEmptyToList(ref fieldList, new Vector2Int(x, y));
            }
        }
        return fieldList;
    }

    private bool AddEmptyToList(ref List<Field> fieldList, Vector2Int index)
    {
        var fieldGameObject = LevelManager.Instance.FieldArray[index.x, index.y];
        Field field = fieldGameObject.GetComponent<Field>();

        if (field.State == FieldStates.FigureEmpty)
        {
            fieldList.Add(field);
            //Debug.Log($"empty ({index.x} / {index.y})");
            return true;
        }

        return false;
    }
}
