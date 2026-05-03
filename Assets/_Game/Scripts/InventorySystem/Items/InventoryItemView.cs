using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItemView : NightstandView
    {
        [field: SerializeField] public SpriteApplyer SpriteApplyer { get; private set; }
        private RectTransform _rectTransform => transform as RectTransform;

        private Vector2 _startPos;
        
        private void Awake()
        {
            _startPos = _rectTransform.anchoredPosition;
        }

        public void Uppercase()
        {
            Vector2 newPos = new Vector2(_startPos.x, _startPos.y + 40f);

            _rectTransform.DOAnchorPos(newPos, 0.25f);
        }

        public void Lowercase()
        {
            _rectTransform.DOAnchorPos(_startPos, 0.25f);
        }
    }
}