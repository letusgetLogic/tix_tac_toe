using TMPro;
using UnityEngine;

namespace CardComponents
{
    public class AmountDisplay : MonoBehaviour
    {
        public TextMeshProUGUI NumberText;

        
        /// <summary>
        /// Initializes the amount of card.
        /// </summary>
        /// <param name="amount"></param>
        public void InitializeAmount(int amount)
        {
            NumberText.text = amount.ToString();
        }
    }
}