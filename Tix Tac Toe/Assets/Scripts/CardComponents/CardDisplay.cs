using GlobalComponents;
using TMPro;
using UnityEngine;

namespace CardComponents
{
    public class CardDisplay : MonoBehaviour
    {
        public GameObject cardPrefab;
        public GameObject GroundBuffBasis;
        public GameObject GroundBuffMultiplier;
        public GameObject GroundDebuffBasis;
        public GameObject GroundRemove;
        public GameObject MakeUpSprite;

        public TextMeshProUGUI CardTypeText;

        public TextMeshProUGUI RemoveSymbolText;

        public TextMeshProUGUI ValueText;
        public TextMeshProUGUI MinusText;
        public TextMeshProUGUI PlusText;
        public TextMeshProUGUI TimesText;

        public TextMeshProUGUI AttributeTypeText;

        public GameObject InfoBoxLeft;
        public GameObject InfoBoxRight;
        public TextMeshProUGUI UseTypeText;
        public TextMeshProUGUI DescriptionText;


        private string cardType;
        private string value;
        private string attributeType;

        private string useType;
        private string description;


        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            GroundBuffBasis.SetActive(false);
            GroundBuffMultiplier.SetActive(false);
            GroundDebuffBasis.SetActive(false);
            GroundRemove.SetActive(false);
            MakeUpSprite.SetActive(false);

            CardTypeText.text = "";

            RemoveSymbolText.enabled = false;

            ValueText.enabled = false;
            MinusText.enabled = false;
            PlusText.enabled = false;
            TimesText.enabled = false;

            AttributeTypeText.text = "";

            InfoBoxLeft.SetActive(false);
            InfoBoxRight.SetActive(false);
            UseTypeText.text = "";
            DescriptionText.text = "";
        }

        /// <summary>
        /// Visual components of the card type Buff Basis.
        /// </summary>
        /// <param name="buffBasisPoints"></param>
        public void BuffBasis(int buffBasisPoints)
        {
            GroundBuffBasis.SetActive(true);
            MakeUpSprite.SetActive(true);

            CardTypeText.text = "Buff";

            ValueText.enabled = true;
            PlusText.enabled = true;
            ValueText.text = buffBasisPoints.ToString(); // Generated Value.

            AttributeTypeText.text = "Basis";

            UseTypeText.text = "Permanent";
            DescriptionText.text = "Card is triggered by every horizontal or vertical row.";
        }

        /// <summary>
        /// Visual components of the card type Buff Multiplier.
        /// </summary>
        /// <param name="buffMultiplierPoints"></param>
        public void BuffMultiplier(float buffMultiplierPoints)
        {
            GroundBuffMultiplier.SetActive(true);
            MakeUpSprite.SetActive(true);

            CardTypeText.text = "Buff";

            ValueText.enabled = true;
            TimesText.enabled = true;
            ValueText.text = buffMultiplierPoints.ToString(); // Generated Value.

            AttributeTypeText.text = "Multiplier";

            UseTypeText.text = "Permanent";
            DescriptionText.text = "Card is triggered by every diagonal row.";
        }

        /// <summary>
        /// Visual components of the card type Debuff Basis.
        /// </summary>
        /// <param name="debuffBasisPoints"></param>
        public void DebuffBasis(int debuffBasisPoints)
        {
            GroundDebuffBasis.SetActive(true);
            MakeUpSprite.SetActive(true);

            CardTypeText.text = "Debuff";

            ValueText.enabled = true;
            MinusText.enabled = true;
            ValueText.text = debuffBasisPoints.ToString(); // Generated Value.

            AttributeTypeText.text = "Basis";

            UseTypeText.text = "Permanent";
            DescriptionText.text = "Your opponent gets debuff basis points once per round.";
        }

        /// <summary>
        /// Visual components of the card type Remove Card.
        /// </summary>
        public void RemoveCard()
        {
            GroundRemove.SetActive(true);

            CardTypeText.text = "Remove";

            RemoveSymbolText.enabled = true;

            AttributeTypeText.text = "Card";

            UseTypeText.text = "Single Use";
            DescriptionText.text = "Remove your card or opponent card.";
        }

        /// <summary>
        /// Visual components of the card type Remove Field.
        /// </summary>
        public void RemoveField()
        {
            GroundRemove.SetActive(true);

            CardTypeText.text = "Remove";

            RemoveSymbolText.enabled = true;

            AttributeTypeText.text = "Field";

            UseTypeText.text = "Single Use";
            DescriptionText.text = "Make a field empty.";
        }

        /// <summary>
        /// Visual components of the card type Remove Points.
        /// </summary>
        public void RemovePoints()
        {
            GroundRemove.SetActive(true);

            CardTypeText.text = "Remove";

            RemoveSymbolText.enabled = true;

            AttributeTypeText.text = "Points";

            UseTypeText.text = "Single Use";
            DescriptionText.text = "Remove opponent scored points.";
        }

        /// <summary>
        /// Sets the sorting layer of all Sprite Render and Canvas.
        /// </summary>
        public void SetSortingLayer()
        {
            SortingLayerUtils.SetSortingLayerInChildrenSpriteRenderer(cardPrefab, "PanelCard");
            SortingLayerUtils.SetSortingLayerInChildrenCanvas(cardPrefab, "PanelCard");
            MakeUpSprite.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "PanelCard"; //MakeUpValueSprite
            SortingLayerUtils.SetSortingLayerInChildrenSpriteRenderer(InfoBoxLeft, "PanelCard");
            SortingLayerUtils.SetSortingLayerInChildrenCanvas(InfoBoxLeft, "PanelCard"); 
            SortingLayerUtils.SetSortingLayerInChildrenSpriteRenderer(InfoBoxRight, "PanelCard");
            SortingLayerUtils.SetSortingLayerInChildrenCanvas(InfoBoxRight, "PanelCard");
        }

        private void OnMouseOver()
        {
            if (Camera.main != null)
            {
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(cardPrefab.transform.position);

                if (viewportPos.x < 0.5f) // Shows InfoBox right next to the card.
                {
                    InfoBoxLeft.SetActive(false);
                    InfoBoxRight.SetActive(true);
                }
                else
                {
                    InfoBoxRight.SetActive(false);
                    InfoBoxLeft.SetActive(true);
                }
            }
            else Debug.LogWarning("No main camera found");
        }

        private void OnMouseExit()
        {
            InfoBoxLeft.SetActive(false);
            InfoBoxRight.SetActive(false);
        }
    }
}