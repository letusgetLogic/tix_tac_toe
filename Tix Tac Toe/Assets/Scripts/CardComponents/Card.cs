using UnityEngine;

namespace CardComponents
{
    public class Card : MonoBehaviour
    {
        [HideInInspector] public CardType CardTypeVar;
        [HideInInspector] public UseType UseTypeVar;
        [HideInInspector] public float Point;
        [HideInInspector] public int Amount;
        
        public void InitializeData(CardType cardTypeVar, UseType useTypeVar, float point, int amount)
        {
            this.CardTypeVar = cardTypeVar;
            this.UseTypeVar = useTypeVar;
            this.Point = point;
            this.Amount = amount;
        }
    }
    
   
}