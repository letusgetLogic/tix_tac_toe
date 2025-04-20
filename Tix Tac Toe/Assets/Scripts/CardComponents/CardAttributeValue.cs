namespace CardComponents
{
    public static class CardAttributeValue
    {
        // For the extension, pls note the "lengthIndex1" in CardDeck.cs and the initialization of amount in inspector.
        
        // 0 and 0f are placeholder in this context.
        public static readonly int[] BuffBasisPoints = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        public static readonly int[] DebuffBasisPoints = {0, 0, 0, 3, 4, 5, 6, 7};
        
        public static readonly float[] BuffMultiplierPoints =
        {
            0f, 
            1.1f, 
            1.2f, 
            1.3f, 
            1.4f, 
            1.5f, 
            1.6f, 
            1.7f, 
            1.8f, 
            1.9f,
            2f
        };

        public static readonly string[] RemoveTypeArray = { "Card" };
    }
}