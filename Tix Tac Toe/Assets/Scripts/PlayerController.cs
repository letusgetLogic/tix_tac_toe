using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float overlapRadius = 0.2f;
        
    [SerializeField] private Transform movePoint;
        
    public LayerMask StopsMovementLayer;
        
    private PlayerInput playerInput;

    private Vector2 moveDirection;

    private string objectTag;
        
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        movePoint.parent = null;
        GetPlayerTag();
    }

    /// <summary>
    /// Gets the game object's player tag.
    /// </summary>
    private void GetPlayerTag()
    {
        objectTag = gameObject.tag;
    }

    /// <summary>
    /// Update method.
    /// </summary>
    private void Update()
    {
        Move();
    }
        
    /// <summary>
    /// On move method.
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Sets the player's figure.
    /// </summary>
    /// <param name="context"></param>
    public void OnSet(InputAction.CallbackContext context)
    {
        if (objectTag == "PlayerX" && TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerX)
        {
            Vector3 position = gameObject.GetComponent<Transform>().position;
            
            GameObject fieldGameObject = LevelManager.Instance.FieldArray[(int)position.x, (int)position.y];
            
            Field field = fieldGameObject.GetComponent<Field>();
            
            field.CheckInput();
        }
        else if (objectTag == "PlayerO" && TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerO)
        {
            Vector3 position = gameObject.GetComponent<Transform>().position;
            
            GameObject fieldGameObject = LevelManager.Instance.FieldArray[(int)position.x, (int)position.y];
            
            Field field = fieldGameObject.GetComponent<Field>();
            
            field.CheckInput();
        }
    }

    /// <summary>
    /// Move method.
    /// </summary>
    private void Move()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.1f)
        {
            Vector3 lastPosition = movePoint.position;
                
            if (Mathf.Abs(moveDirection.x) == 1f && !IsOutHorizontal())
            {
                movePoint.position += new Vector3(moveDirection.x, 0f, 0f);
            }
            else if (Mathf.Abs(moveDirection.y) == 1f && !IsOutVertical())
            {
                movePoint.position += new Vector3(0f, moveDirection.y, 0f);
            }

            //This code line blocks player to come near of other player.
            //if (IsOutHorizontal() || IsOutVertical()) movePoint.position = lastPosition;
        }
    }

    /// <summary>
    /// Is move point overlapped StopMovementLayer?
    /// </summary>
    /// <returns></returns>
    private bool IsOutHorizontal()
    {
        Vector3 newMovePosition = movePoint.position + new Vector3(moveDirection.x, 0f, 0f);
          
        return Physics2D.OverlapCircle(newMovePosition, overlapRadius, StopsMovementLayer);
    }
        
    /// <summary>
    /// Is move point overlapped StopMovementLayer?
    /// </summary>
    /// <returns></returns>
    private bool IsOutVertical()
    {
        Vector3 newMovePosition = movePoint.position + new Vector3(0f, moveDirection.y, 0f);
            
        return Physics2D.OverlapCircle(newMovePosition, overlapRadius, StopsMovementLayer);
    }
}