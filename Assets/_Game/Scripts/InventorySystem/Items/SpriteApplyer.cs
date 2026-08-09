using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.InventorySystem
{
    public class SpriteApplyer : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private SpriteStorage _spriteStorage;

        private Vector2 _spriteSize;
        
        public void Construct(SpriteStorage spriteStorage, Vector2 spriteSize = default)
        {
            _spriteSize = spriteSize;
            _spriteStorage = spriteStorage;
            
            _spriteStorage.Sprite.Subscribe(SetSprite);
        }

        private void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            
            if (_spriteSize == default)
                _image.SetNativeSize();
            else
                _image.rectTransform.sizeDelta = new Vector2(_spriteSize.x, _spriteSize.y);
        }

        private void OnDestroy()
        {
            _spriteStorage.Sprite.Unsubscribe(SetSprite);
        }
    }
}