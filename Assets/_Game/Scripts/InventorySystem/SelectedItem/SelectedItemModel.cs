using UniRx;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SelectedItemModel
    {
        public readonly SpriteStorage SpriteStorage;
        public readonly ReactiveProperty<Color> BackgroundColor;
        public readonly ReactiveProperty<bool> IsActive;
        public readonly ReactiveProperty<Vector2> Position;
        
        public SelectedItemModel()
        {
            Position = new ReactiveProperty<Vector2>();
            BackgroundColor = new ReactiveProperty<Color>(Color.white);
            SpriteStorage = new SpriteStorage(null);
            IsActive = new ReactiveProperty<bool>(false);
        }
    }
}