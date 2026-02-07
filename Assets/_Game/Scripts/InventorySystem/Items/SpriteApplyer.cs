using System;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SpriteApplyer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private SpriteStorage _spriteStorage;
        
        public void Construct(SpriteStorage spriteStorage)
        {
            _spriteStorage = spriteStorage;
            
            _spriteStorage.Sprite.Subscribe(SetSprite);
        }

        private void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        private void OnDestroy()
        {
            _spriteStorage.Sprite.Unsubscribe(SetSprite);
        }
    }
}