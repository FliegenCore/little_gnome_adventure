using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private RectTransform _inventoryBackground;
        [SerializeField] private CellView[] _cells;
        
        private Sequence _openAnimationSequence;

        private ReactiveProperty<bool> _isOpen;
        
        public void Construct(ReactiveProperty<bool> isOpen)
        {
            _isOpen = isOpen;

            _isOpen.Subscribe(Open).AddTo(gameObject);
        }

        private void Open(bool isOpen)
        {
            if(isOpen)
                Show();
            else
                Hide();
        }

        private void SetTitleText(string title)
        {
            
        }
        
        private void Show()
        {
            if (_openAnimationSequence != null)
                DOTween.Kill(_openAnimationSequence);
            
            _openAnimationSequence = DOTween.Sequence();

            _openAnimationSequence.Append(_inventoryBackground.DOAnchorPosY(Inventory.VIEW_POS, 0.25f));
        }

        private void Hide()
        {
            if (_openAnimationSequence != null)
                DOTween.Kill(_openAnimationSequence);
            
            _openAnimationSequence = DOTween.Sequence();
            _openAnimationSequence.Append(_inventoryBackground.DOAnchorPosY(-Inventory.VIEW_POS, 0.25f));
        }
    }
}