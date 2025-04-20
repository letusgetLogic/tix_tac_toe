using UnityEngine;

namespace GlobalComponents
{
    public static class SortingLayerUtils
    {
        public static void SetSortingLayerInChildrenSpriteRenderer(GameObject parentGameObject, string sortingLayerName)
        {
            SpriteRenderer[] spriteRenderers = parentGameObject.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sortingLayerName = sortingLayerName;
            }
        }

        public static void SetSortingLayerInChildrenCanvas(GameObject parentGameObject, string sortingLayerName)
        {
            Canvas[] canvases = parentGameObject.GetComponentsInChildren<Canvas>();

            foreach (Canvas canvas in canvases)
            {
                canvas.sortingLayerName = sortingLayerName;
            }
        }
    }
}