using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.InventorySystem
{
    public class SpriteApplyer : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private SpriteStorage _spriteStorage;
        
        public void Construct(SpriteStorage spriteStorage)
        {
            _spriteStorage = spriteStorage;
            
            _spriteStorage.Sprite.Subscribe(SetSprite);
        }

        private void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        private void OnDestroy()
        {
            _spriteStorage.Sprite.Unsubscribe(SetSprite);
        }
    }
}