using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace CardComponents
{
    public class CardDeckManager : MonoBehaviour
    {
        [Header("GameObjects References")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject slotCardDraftX1;
        [SerializeField] private GameObject slotCardDraftX2;
        [SerializeField] private GameObject slotCardDraftX3;
        [SerializeField] private GameObject slotCardDraftO1;
        [SerializeField] private GameObject slotCardDraftO2;
        [SerializeField] private GameObject slotCardDraftO3;
        
        public List<GameObject> Cards = new List<GameObject>();
        public List<GameObject> CardDraft = new List<GameObject>();

        
        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
           
        }
        
        /// <summary>
        /// Start method.
        /// </summary>
        private void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /// <summary>
        /// Checks the amount of cards in the list.
        /// </summary>
        public void CardAmount()
        {
            Debug.Log("The amount of cards in the list: " + Cards.Count);
        }

        /// <summary>
        /// Generates the drafting cards.
        /// </summary>
        public void GenerateCardDraft()
        {
            if (Cards.Count > 0) // "GameObject Card" from Cards to CardDraft. 
            {
                int randomIndex1 = Random.Range(0, Cards.Count);
                CardDraft.Add(Cards[randomIndex1]);
                Cards.RemoveAt(randomIndex1);
            }
          
            if (Cards.Count > 0) // "GameObject Card" from Cards to CardDraft. 
            {
                int randomIndex2 = Random.Range(0, Cards.Count);
                CardDraft.Add(Cards[randomIndex2]);
                Cards.RemoveAt(randomIndex2);
            }
          
            if (Cards.Count > 0) // "GameObject Card" from Cards to CardDraft. 
            {
                int randomIndex3 = Random.Range(0, Cards.Count);
                CardDraft.Add(Cards[randomIndex3]);
                Cards.RemoveAt(randomIndex3);
            }
            
            SetCardToPosition();
        }

        /// <summary>
        /// Sets card's position to slot's position.
        /// </summary>
        private void SetCardToPosition()
        {
            GameObject slotDraft1 = null;
            GameObject slotDraft2 = null;
            GameObject slotDraft3 = null;
            
            switch (TurnManager.Instance.CurrentPlayerTurn) // Initialization of slotDraft of player, who is turn.
            {
                case TurnStates.PlayerX:
                    slotDraft1 = slotCardDraftX1;
                    slotDraft2 = slotCardDraftX2;
                    slotDraft3 = slotCardDraftX3;
                    return;
                
                case TurnStates.PlayerO:
                    slotDraft1 = slotCardDraftO1;
                    slotDraft2 = slotCardDraftO2;
                    slotDraft3 = slotCardDraftO3;
                    return;
                
                case TurnStates.PlayerEmpty:
                    Debug.Log("GenerateCardDraft() while TurnStates.PlayerEmpty -> break;");
                    break;
            }
            
            if (CardDraft[0] != null && slotDraft1 != null) 
            {
                CardDraft[0].transform.position = slotDraft1.transform.position;
            }
            else if (CardDraft[0] == null) Debug.Log("CardDraft[0] = null.");
            
            else if (slotDraft1   == null) Debug.Log("slotDraft1   = null.");
            
            if (CardDraft[1] != null && slotDraft2 != null) 
            {
                CardDraft[1].transform.position = slotDraft2.transform.position;
            }
            else if (CardDraft[1] == null) Debug.Log("CardDraft[1] = null.");
            
            else if (slotDraft2   == null) Debug.Log("slotDraft2   = null.");
            
            if (CardDraft[2] != null && slotDraft3 != null) 
            {
                CardDraft[2].transform.position = slotDraft3.transform.position;
            }
            else if (CardDraft[2] == null) Debug.Log("CardDraft[2] = null.");
            
            else if (slotDraft3   == null) Debug.Log("slotDraft3   = null.");
        }
    }
}
