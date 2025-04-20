using TMPro;
using UnityEngine;

namespace CardComponents
{ 
    public class CardDeck : MonoBehaviour
    {
        [Header("Should the amount display in the panel be updating per draw?")]
        [SerializeField] public bool IsPanelUpdating;
        
        [Header("GameObjects References")]
        public GameObject DeckPanel;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject amountDisplayPrefab;
        [SerializeField] private GameObject slotPanelRow1;
        [SerializeField] private GameObject slotPanelRow2;
        [SerializeField] private GameObject slotPanelRow3;
        public TextMeshProUGUI DeckSizeAmountText;
        
        [Header("Scripts References")]
        [SerializeField] private CardDeckManager cardDeckManager;
        
        [Header("vvv    CARD DECK    vvv")]
        [Header("Initialization of amount of cards (Deck Size must manual change)")]
        //                           BuffBasis + BuffMultiplier + DebuffBasis + RemoveCard
        public int DeckSize = 170; //  55      +       55       +      30     +    30 
        
        [HideInInspector] public int DeckSizeAmount;
       
        
        [Header("(pls note the value indexes for each card type in CardAttributeValue.cs)")]
        [SerializeField] public int[] BuffBasisAmount = new int[CardAttributeValue.BuffBasisPoints.Length];
        [SerializeField] public int[] BuffMultiplierAmount = new int[CardAttributeValue.BuffMultiplierPoints.Length];
        [SerializeField] public int[] DebuffBasisAmount = new int[CardAttributeValue.DebuffBasisPoints.Length];
        [SerializeField] public int RemoveCardAmount;
        
        
        public GameObject[,] CardGameObject;
        public GameObject[,] CardAmountGameObject;
        private readonly int lengthIndex0 = 5;
        private readonly int lengthIndex1 = 11;
        
        private readonly Vector3 corePosition = new (4, 3, 0);
        private readonly Vector3 hidingPosition = new (4, -14, 0);
        private readonly float slotDistance = 2f;
        private readonly int firstIndexDebuffBasisPoint = 3;
        
        private bool isPanelHiding;
        
        private int panelRow;
        
        Card card;
        CardDisplay cardDisplay;
        AmountDisplay amountDisplay;
        
        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
            CardGameObject = new GameObject[lengthIndex0, lengthIndex1];
            CardAmountGameObject = new GameObject[lengthIndex0, lengthIndex1];

            isPanelHiding = true;
            
            DeckSizeAmount = DeckSize;
            DeckSizeAmountText.text = DeckSizeAmount.ToString();
            
            CreateCardDeck();
        }
        
        /// <summary>
        /// Shows and hides the deck panel.
        /// </summary>
        private void OnMouseDown()
        {
            isPanelHiding = !isPanelHiding;
            DeckPanel.transform.position = isPanelHiding ? hidingPosition : corePosition;
        }
        
        /// <summary>
        /// Creates cards for the deck.
        /// </summary>
        private void CreateCardDeck()
        {
            CreateCardOnPanelRow1();
            CreateCardOnPanelRow2();
            CreateCardOnPanelRow3();
            cardDeckManager.CardAmount();
        }
        
        /// <summary>
        /// Creates BuffBasis cards on the panel row 1 and adds to the list.
        /// </summary>
        private void CreateCardOnPanelRow1()
        {
            panelRow = 1;
            
            for (int i = 1; i < CardAttributeValue.BuffBasisPoints.Length; i++)
            {
                // Position for the card in the panel, i - 1 because at the beginning position i = 1 not 0.
                
                float slotAxisX = slotPanelRow1.transform.position.x + slotDistance * (i - 1); 
                
                Vector3 slotPosition = 
                    new Vector3(slotAxisX, slotPanelRow1.transform.position.y, slotPanelRow1.transform.position.z);
                
                // Creation of card in 2D Array and sets the prefab position in the panel.
                CardGameObject[panelRow, i] = 
                    Instantiate(cardPrefab, slotPosition, Quaternion.identity, slotPanelRow1.transform);
                
                // Creation of amount display in 2D Array and sets the prefab position in the panel.
                CardAmountGameObject[panelRow, i] = 
                    Instantiate(amountDisplayPrefab, slotPosition, Quaternion.identity, slotPanelRow1.transform);
                
                AccessScriptsFromGameObjects(panelRow, i);
                
                card.InitializeData(
                    CardType.BuffBasis, 
                    UseType.Permanent,
                    CardAttributeValue.BuffBasisPoints[i], 
                    BuffBasisAmount[i]);

                // Creation of the visual components.
                cardDisplay.BuffBasis(CardAttributeValue.BuffBasisPoints[i]);
                cardDisplay.SetSortingLayer();
                amountDisplay.InitializeAmount(BuffBasisAmount[i]);
                
                AddToList(BuffBasisAmount[i], panelRow, i);
            }
        }

        /// <summary>
        /// Creates BuffMultiplier cards on the panel row 2 and adds to the list.
        /// </summary>
        private void CreateCardOnPanelRow2()
        {
            panelRow = 2;
            
            for (int i = 1; i < CardAttributeValue.BuffMultiplierPoints.Length; i++)
            {
                // Position for the card in the panel, i - 1 because at the beginning position i = 1 not 0.
                
                float slotAxisX = slotPanelRow2.transform.position.x + slotDistance * (i - 1); 
                
                Vector3 slotPosition = 
                    new Vector3(slotAxisX, slotPanelRow2.transform.position.y, slotPanelRow2.transform.position.z);
                
                // Creation of card in 2D Array and sets the prefab position in the panel.
                CardGameObject[panelRow, i] = 
                    Instantiate(cardPrefab, slotPosition, Quaternion.identity, slotPanelRow2.transform);
                
                // Creation of amount display in 2D Array and sets the prefab position in the panel.
                CardAmountGameObject[panelRow, i] = 
                    Instantiate(amountDisplayPrefab, slotPosition, Quaternion.identity, slotPanelRow2.transform);
                
                AccessScriptsFromGameObjects(panelRow, i);
                
                card.InitializeData(
                    CardType.BuffMultiplier, 
                    UseType.Permanent,
                    CardAttributeValue.BuffMultiplierPoints[i], 
                    BuffMultiplierAmount[i]);

                // Creation of the visual components.
                cardDisplay.BuffMultiplier(CardAttributeValue.BuffMultiplierPoints[i]);
                cardDisplay.SetSortingLayer();
                amountDisplay.InitializeAmount(BuffMultiplierAmount[i]);
                
                AddToList(BuffMultiplierAmount[i], panelRow, i);
            }
        }
        
        /// <summary>
        /// Creates cards on the panel row 3 and adds to the list.
        /// </summary>
        private void CreateCardOnPanelRow3()
        {
            panelRow = 3;
            
            int totalLength = CardAttributeValue.DebuffBasisPoints.Length + CardAttributeValue.RemoveTypeArray.Length;
            
            for (int i = firstIndexDebuffBasisPoint; i < totalLength; i++)
            {
                // Position for the card in the panel, i - firstIndex because at the beginning i = firstIndex not 0.
                
                float slotAxisX = slotPanelRow3.transform.position.x + slotDistance * (i - firstIndexDebuffBasisPoint);
                
                Vector3 slotPosition = 
                        new Vector3(slotAxisX, slotPanelRow3.transform.position.y, slotPanelRow3.transform.position.z);
                
                // Creation of card in 2D Array and sets the prefab position in the panel.
                CardGameObject[panelRow, i] = 
                                    Instantiate(cardPrefab, slotPosition, Quaternion.identity, slotPanelRow3.transform);
                
                // Creation of amount display in 2D Array and sets the prefab position in the panel.
                CardAmountGameObject[panelRow, i] = 
                         Instantiate(amountDisplayPrefab, slotPosition, Quaternion.identity, slotPanelRow3.transform);
                
                
                AccessScriptsFromGameObjects(panelRow, i);
                
                
                // Data initialization of DebuffBasis and RemoveCard,
                // creation of the visual components,
                // adding to the list the amount of card.
                
                if (i < CardAttributeValue.DebuffBasisPoints.Length) // Amount of the cards Debuff on row 3.
                {
                    card.InitializeData(
                        CardType.DebuffBasis, 
                        UseType.Permanent,
                        CardAttributeValue.DebuffBasisPoints[i], 
                        DebuffBasisAmount[i]);
                    
                    cardDisplay.DebuffBasis(CardAttributeValue.DebuffBasisPoints[i]);
                    cardDisplay.SetSortingLayer();
                    amountDisplay.InitializeAmount(DebuffBasisAmount[i]);
                    
                    AddToList(DebuffBasisAmount[i], panelRow, i);
                }
                else // Amount of the cards Remove on row 3.
                {
                    card.InitializeData(
                        CardType.RemoveCard, 
                        UseType.SingleUse,
                        0, 
                        RemoveCardAmount);
                    
                    cardDisplay.RemoveCard();
                    cardDisplay.SetSortingLayer();
                    amountDisplay.InitializeAmount(RemoveCardAmount);
                    
                    AddToList(RemoveCardAmount, panelRow, i);
                }
            }
        }

        /// <summary>
        /// Access scripts from game objects.
        /// </summary>
        private void AccessScriptsFromGameObjects(int panelRowIndex, int i)
        {
            card = CardGameObject[panelRowIndex, i].GetComponent<Card>();
            cardDisplay = CardGameObject[panelRowIndex, i].GetComponent<CardDisplay>();
            amountDisplay = CardAmountGameObject[panelRowIndex, i].GetComponent<AmountDisplay>();
        }
        
        /// <summary>
        /// Adds to the list the amount of card.
        /// </summary>
        /// <param name="panelRowIndex"></param>
        /// <param name="i"></param>
        private void AddToList(int cardAmount, int panelRowIndex, int i)
        {
            for (int n = 0; n < cardAmount; n++)
            {
                cardDeckManager.Cards.Add(CardGameObject[panelRowIndex, i]);
            }
        }
    }
}
