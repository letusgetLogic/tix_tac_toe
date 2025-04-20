using UnityEngine;

namespace GlobalComponents
{
     public class CursorChanger : MonoBehaviour
    {
        public Sprite cursorSprite; 

        void Start()
        {
            if (cursorSprite != null)
            {
                Texture2D texture = SpriteToTexture(cursorSprite);
                Vector2 hotSpot = new Vector2(6, 5);
                Cursor.SetCursor(texture, hotSpot, CursorMode.Auto);
            }
        }

        Texture2D SpriteToTexture(Sprite sprite)
        {
            Rect rect = sprite.textureRect;
            Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
            texture.SetPixels(sprite.texture.GetPixels((int)rect.x, (int)rect.y, 
                                (int)rect.width, (int)rect.height));
            texture.Apply();
            return texture;
        }
    }
}