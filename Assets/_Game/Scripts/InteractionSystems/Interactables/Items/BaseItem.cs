using System;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations;
using Core.Common;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItem : AbstractInteractable
    {
        private readonly InventoryProxy _inventoryProxy;
        private readonly BaseItemView _baseItemView;
        private readonly IPlayerFactory _playerFactory;
        
        private bool _isTaked;
        
        public BaseItem(
            BaseItemModel abstractInteractableModel, 
            BaseItemView view, 
            EventBus eventBus,
            InventoryProxy inventory,
            IPlayerFactory playerFactory
            ) 
            : base(abstractInteractableModel, view, eventBus)
        {
            _playerFactory = playerFactory;
            _baseItemView           = view;
            _inventoryProxy         = inventory;
        }

        public override void Interact(Action callback)
        {
            if (_isTaked)
                return;

            _isTaked = true;
            _inventoryProxy.AddItem(Enum.Parse<ItemId>(AbstractInteractableModel.Id));
            AbstractInteractableModel.CanSelected.Value = false;
            if (_baseItemView.AnimationControl != null && _baseItemView.AnimationControl.HasAnimation("take"))
            {
                _baseItemView.AnimationControl.SetAnimation(0, "take", false, Dispose);
            }
            else
            {
                Dispose();
            }
            
            if (_playerFactory != null)
            {
                AnimationControl animationControl = _playerFactory.GetPlayer().PlayerView.AnimationPlayer.AnimationControl;
                
                animationControl.SetAnimation(0, "body/take", false,
                    () =>
                    {
                        animationControl.ResetAnimation(1);
                        callback?.Invoke();
                    });
            }
        }

        public override bool CanInteract()
        {
            if (_isTaked)
                return false;
            
            return AbstractInteractableModel.CanSelected.Value;
        }
    }
}