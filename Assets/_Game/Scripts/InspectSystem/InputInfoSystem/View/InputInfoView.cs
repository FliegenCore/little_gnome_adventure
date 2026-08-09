using _Game.Scripts.InventorySystem;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.InputInfoSystem
{
    public class InputInfoView : MonoBehaviour
    {
        [SerializeField] private InputInfoGroupView _inputInfoGroupViewPrefab;
        [SerializeField] private SpriteApplyer _inputImageSpriteApplyerPrefab;
        
        public InputInfoGroupView InputInfoGroupViewPrefab => _inputInfoGroupViewPrefab;
        public SpriteApplyer InputImageSpriteApplyerPrefab => _inputImageSpriteApplyerPrefab;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}