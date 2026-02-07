using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SpriteStorage
    {
        public readonly Observable<Sprite> Sprite;
        
        public SpriteStorage(Sprite sprite)
        {
            Sprite = new  Observable<Sprite>(sprite);
        }
    }
}