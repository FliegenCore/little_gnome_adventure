using System;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items.Hints
{
    public abstract class AbstractHintSelect : MonoBehaviour
    {
        protected CompositeDisposable _disposable = new CompositeDisposable();
        
        protected ReactiveProperty<bool> _isSelected;
        protected EventBus _eventBus;

        public void Construct(EventBus eventBus, ReactiveProperty<bool> isSelect)
        {
            _eventBus = eventBus;
            _isSelected = isSelect;

            _isSelected.Subscribe(HintSelect).AddTo(_disposable);
        }
        
        protected abstract void HintSelect(bool isSelect);

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}