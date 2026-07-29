using _Game.Scripts.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordInput : InspectInputHandler
    {
        private readonly EventBus _eventBus;
        private readonly HellGatesPasswordModel _hellGatesPasswordModel;
        
        public HellGatesPasswordInput(
            InputSystem_Actions inputSystemActions,
            EventBus eventBus,
            HellGatesPasswordModel hellGatesPasswordModel
            ) : base(inputSystemActions)
        {
            _eventBus               = eventBus;
            _hellGatesPasswordModel = hellGatesPasswordModel;
        }

        public override void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            if (_currentInspectModel.Interactables.Count <= 0)
                return;
            
            SelectFirst();
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
            _inputSystemActions.Player.Interact.performed += Interact;
        }
        
        public override void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _inputSystemActions.Player.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext callback)
        {
            string index = _hellGatesPasswordModel.CurrentIndex.Value.ToString();

            _hellGatesPasswordModel.CurrentPassword.Value += index;
            
            _hellGatesPasswordModel.WritedCount.Value++;
        }
        
        protected override void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();
            
            if (direction.x != 0)
            {
                if (direction.x > 0)
                {
                    SetCurrentIndex(_hellGatesPasswordModel.CurrentIndex.Value + 1);
                }
                else
                {
                    SetCurrentIndex(_hellGatesPasswordModel.CurrentIndex.Value - 1);
                }
            }
            else if (direction.y != 0)
            {
                if (direction.y > 0)
                {
                    SetCurrentIndex(_hellGatesPasswordModel.CurrentIndex.Value + 3);
                }
                else
                {
                    SetCurrentIndex(_hellGatesPasswordModel.CurrentIndex.Value - 3);
                }
            }
        }

        private void SetCurrentIndex(int index)
        {
            if (index < 0)
            {
                _hellGatesPasswordModel.CurrentIndex.Value = 0;
            }
            if (index > 8)
            {
                _hellGatesPasswordModel.CurrentIndex.Value = 8;
            }
            else
            {
                _hellGatesPasswordModel.CurrentIndex.Value = index;
            }
        }
    }
}