using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muster
{
    public class TicTacToe : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button nextRoundButton;

        [Header("Dropdown")]
        [SerializeField] private TMP_Dropdown bestOfDropdown;

        [Header("TextMeshPro")]
        [SerializeField] private TextMeshProUGUI bestOfText;
        [SerializeField] private TextMeshProUGUI roundText;
        [SerializeField] private TextMeshProUGUI playerTurnText;
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI congratulationsText;

        public bool moreRounds;
        [FormerlySerializedAs("PlaygroundButtons")] public Button[] playgroundButtons;

        private string _currentPlayer = "X";
        private int _bestOfNum;
        private int _roundNum;
        private int _pointsNumGoal;
        private int _pointsNumX;
        private int _pointsNumO;

        private void Start()
        {
            startButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(true);
            nextRoundButton.gameObject.SetActive(false);

            bestOfDropdown.gameObject.SetActive(false);

            bestOfText.gameObject.SetActive(false);
            roundText.gameObject.SetActive(false);
            playerTurnText.gameObject.SetActive(true);
            pointsText.gameObject.SetActive(false);
            congratulationsText.gameObject.SetActive(true);

            if (moreRounds) ActivatesObjectsMoreRounds();

            congratulationsText.text = "";

            foreach (Button button in playgroundButtons)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            
            }
        }
        private void Update()
        {
            playerTurnText.text = $"Player {_currentPlayer} is turn...";

            if (moreRounds)
            {
                roundText.text = "Round " + _roundNum;
            }
       
        }
        void ActivatesObjectsMoreRounds()
        {
            startButton.gameObject.SetActive(true);

            bestOfDropdown.gameObject.SetActive(true);

            bestOfText.gameObject.SetActive(true);
            playerTurnText.gameObject.SetActive(false);

            _roundNum = 1;
        }
        void OnButtonClick(Button button)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText.text == "")
            {
                buttonText.text = _currentPlayer;
                if (CheckGameOver()) 
                {
                    nextRoundButton.gameObject.SetActive(true);
                    nextRoundButton.onClick.AddListener(() => OnNextRoundButtonClick());
                    return; 
                }
                _currentPlayer = (_currentPlayer == "X") ? "O" : "X";
            }
        }
        bool CheckGameOver()
        {
            if (CheckWin())
            {
                congratulationsText.text = $"Player {_currentPlayer} wins!";
                return true;
            }
            if (CheckDraw())
            {
                congratulationsText.text = $"It's a draw!";
                return true;
            }
            return false;
        }
        void OnNextRoundButtonClick()
        {
            ResetBoard();
        }
        bool CheckWin()
        {
            // Gewinnbedingungen prüfen (Zeilen, Spalten, Diagonalen)
            int[,] winConditions = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Zeilen
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Spalten
                {0, 4, 8}, {2, 4, 6}             // Diagonalen
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0], b = winConditions[i, 1], c = winConditions[i, 2];
                if (playgroundButtons[a].GetComponentInChildren<TextMeshProUGUI>().text == _currentPlayer &&
                    playgroundButtons[b].GetComponentInChildren<TextMeshProUGUI>().text == _currentPlayer &&
                    playgroundButtons[c].GetComponentInChildren<TextMeshProUGUI>().text == _currentPlayer)
                {
                    foreach (Button button2 in playgroundButtons)
                    {
                        button2.interactable = false;
                        playgroundButtons[a].interactable = true;
                        playgroundButtons[c].interactable = true;
                        playgroundButtons[b].interactable = true;
                    }
                    return true; // Spieler hat gewonnen
                }
            }
            return false;
        }
        bool CheckDraw()
        {
            foreach (Button button in playgroundButtons)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText.text == "") return false;
            }
            return true;
        }

        void ResetBoard()
        {
            foreach (Button button in playgroundButtons)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "";
                button.interactable = true;
            }
            nextRoundButton.gameObject.SetActive(false);
            congratulationsText.text = "";
        }
    }
}

