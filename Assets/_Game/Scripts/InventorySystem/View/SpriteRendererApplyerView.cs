using System;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SpriteRendererApplyerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private SpriteStorage _spriteStorage;
        
        public void Construct(SpriteStorage spriteStorage, ReactiveProperty<bool> isActive)
        {
            _spriteStorage = spriteStorage;
            
            _spriteStorage
                .Sprite
                .Subscribe(SetSprite);
            
            isActive.Subscribe(SetActive).AddTo(gameObject);
        }

        private void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        private void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}