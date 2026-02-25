using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SelectCircleView : MonoBehaviour
    {
        private RectTransform _rectTransform => transform as RectTransform;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void SetAnchoredPosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }
    }
}