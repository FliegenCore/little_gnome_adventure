using UniRx;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SelectedItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundColor;
        [field: SerializeField] public SpriteRendererApplyerView SpriteRendererApplyerView { get; private set; }

        public void Construct(SelectedItemModel selectedItemModel)
        {
            SpriteRendererApplyerView.Construct(selectedItemModel.SpriteStorage, selectedItemModel.IsActive);
            selectedItemModel.BackgroundColor.Subscribe(SetBackgroundColor).AddTo(gameObject);
            selectedItemModel.Position.Subscribe(SetPosition).AddTo(gameObject);
        }

        private void SetBackgroundColor(Color color)
        {
            _backgroundColor.color = color;
        }

        private void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}