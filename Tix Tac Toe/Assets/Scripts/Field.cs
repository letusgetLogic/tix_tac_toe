using Enums;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject SpriteComponents;
    [SerializeField] private GameObject xSprite;
    [SerializeField] private GameObject xSpriteBack;
    [SerializeField] private GameObject oSprite;
    [SerializeField] private GameObject oSpriteBack;
    [SerializeField] private GameObject makeUpSprite;

    [SerializeField] private AudioSource soundX;
    [SerializeField] private AudioSource soundO;

    [HideInInspector] public int IndexVertical;
    [HideInInspector] public int IndexHorizontal;
    [HideInInspector] public FieldStates State;

    private bool isClickable;
        
    
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        gameObject.SetActive(true);
        xSprite.SetActive(false);
        xSpriteBack.SetActive(false);
        oSprite.SetActive(false);
        oSpriteBack.SetActive(false);
        makeUpSprite.SetActive(false);
        isClickable = true;
    }
        
    /// <summary>
    /// Initializes data of field.
    /// </summary>
    /// <param name="fieldIndexVertical"></param>
    /// <param name="fieldIndexHorizontal"></param>
    /// <param name="fieldState"></param>
    public void InitializeData(int fieldIndexVertical, int fieldIndexHorizontal, FieldStates fieldState)
    {
        this.IndexVertical = fieldIndexVertical;
        this.IndexHorizontal = fieldIndexHorizontal;
        this.State = fieldState;
    }
    
    /// <summary>
    /// On mouse down event.
    /// </summary>
    public void OnMouseDown()
    {
        if (!GameManager.Instance.IsClickingActive) return;
        
        CheckInput();
    }

    /// <summary>
    /// Checks the conditions and the inputs.
    /// </summary>
    public void CheckInput()
    {
        if (TurnManager.Instance.PlayerIsTurn || 
            !isClickable || 
            LevelManager.CurrentState != LevelState.Play)
        {
            return;
        }

        if (GameManager.Instance.IsBotActive &&
            TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerO)
        {
            return;
        }
        
        isClickable = false;
        TurnManager.Instance.PlayerIsTurn = true;
            
        ActivateFieldBehaviour();
    }

    /// <summary>
    /// Activates field's behaviour.
    /// </summary>
    public void ActivateFieldBehaviour()
    {
        if (!LevelManager.Instance.GameOver)
        {
            if (State == FieldStates.FigureEmpty)
            {
                SetFieldState();

                CheckScoreConditions.Instance.InitializeData(IndexHorizontal, IndexVertical, State);
                    
                AnimationManager.Instance.StartAnimation();
            }
        }
    }

    /// <summary>
    /// Sets the state of field.
    /// </summary>
    private void SetFieldState()
    {
        makeUpSprite.SetActive(true);

        if (TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerX)
        {
            OnEnableX_Sprite();
                        
            State = FieldStates.FigureX;
                        
            PlaySoundX();
                        
            UIManager.Instance.MovesPlayerX++;
        }
        else if (TurnManager.Instance.CurrentPlayerTurn == TurnStates.PlayerO)
        {
            OnEnableO_Sprite();
                        
            State = FieldStates.FigureO;
                        
            PlaySoundO();
                        
            UIManager.Instance.MovesPlayerO++;
        }
    }
    
    /// <summary>
    /// Sets X-Sprite active.
    /// </summary>
    private void OnEnableX_Sprite()
    {
        oSprite.SetActive(false);
        oSpriteBack.SetActive(false);
                        
        xSprite.SetActive(true);
        xSpriteBack.SetActive(true);
    }

    /// <summary>
    /// Sets O-Sprite active.
    /// </summary>
    private void OnEnableO_Sprite()
    {
        xSprite.SetActive(false);
        xSpriteBack.SetActive(false);
                        
        oSprite.SetActive(true);
        oSpriteBack.SetActive(true);
    }

    /// <summary>
    /// Plays sound while sets X.
    /// </summary>
    private void PlaySoundX()
    {
        soundX.PlayOneShot(soundX.clip);
    }

    /// <summary>
    /// Plays sound while sets O.
    /// </summary>
    private void PlaySoundO()
    {
        soundO.PlayOneShot(soundO.clip);
    }

}