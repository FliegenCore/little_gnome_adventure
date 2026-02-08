using _Game.Scripts.InteractionSystems.HintSystem.Signals;
using Core.Common;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.InteractionSystems.Interactables.Items.Hints
{
    public class OutlineHintSelect : AbstractHintSelect
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        
        
        protected override void HintSelect(bool isSelect)
        {
            if(isSelect)
                _eventBus.TriggerEvenet<SetOutlineMaterialSignal, SpriteRenderer>(_spriteRenderer);
            else
                _eventBus.TriggerEvenet<SetDefaultMaterialSignal, SpriteRenderer>(_spriteRenderer);
        }
    }
}