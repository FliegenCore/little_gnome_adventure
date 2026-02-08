using System;
using _Game.Scripts.InteractionSystems.HintSystem.Signals;
using Core.Common;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.InteractionSystems.HintSystem
{
    public class OutlineHintController : IInitializable, IDisposable
    {
        private readonly Material _baseMaterial;
        private readonly Material _outlineMaterial;

        private readonly EventBus _eventBus;
        
        private OutlineHintController(EventBus eventBus)
        {
            _eventBus = eventBus;
            
            _outlineMaterial = new Material(Shader.Find("Sprites/Outline"));
            _baseMaterial = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<SetOutlineMaterialSignal, SpriteRenderer>(this, AddOutline);
            _eventBus.Subscribe<SetDefaultMaterialSignal, SpriteRenderer>(this, RemoveOutline);
        }
        
        private void AddOutline(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.material = _outlineMaterial;
        }

        private void RemoveOutline(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.material = _baseMaterial;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SetOutlineMaterialSignal>(this);
            _eventBus.Unsubscribe<SetDefaultMaterialSignal>(this);
        }

        
    }
}