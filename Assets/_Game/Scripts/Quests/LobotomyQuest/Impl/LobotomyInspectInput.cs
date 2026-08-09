using _Game.Scripts.InspectSystem;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.Quests.LobotomyQuest.Impl.Needle;
using _Game.Scripts.RoomSystems.InputInfoSystem;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl
{
    public class LobotomyInspectInput : InspectInputHandler
    {
        public LobotomyInspectInput(
            InputSystem_Actions inputSystemActions,
            EventBus eventBus
            ) : base(inputSystemActions, eventBus)
        {
        }
        
        public override void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            if (_currentInspectModel.Interactables.Count <= 0)
                return;

            InputInfoGroup group1 = new InputInfoGroup("перемещение", EKeyIndex.A, EKeyIndex.D);
            InputInfoGroup group2 = new InputInfoGroup("глубина", EKeyIndex.S, EKeyIndex.W);
            InputInfoGroup group3 = new InputInfoGroup("подсказка", EKeyIndex.E);
            InputInfoGroup group4 = new InputInfoGroup("выход", EKeyIndex.Esc);
            
            _eventBus.TriggerEvenet<ShowInputInfoViewSignal, InputInfoGroup[]>(new[] { group4, group3, group2, group1 });
            
            SelectFirst();
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
            _inputSystemActions.Player.Interact.performed += Interact;
        }
        
        public override void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _inputSystemActions.Player.Interact.performed -= Interact;
            
            _eventBus.TriggerEvenet<HideInputInfoViewSignal>();
            
            if (_selectedInteractable != null)
                _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;
        }

        private void Interact(InputAction.CallbackContext callback)
        {
            _eventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>("Instruction");
        }
        
        protected override void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();
            
            if (direction.x != 0)
            {
                if (HasMoreItem())
                {
                    AbstractInteractable newModel = GetInteractableByDirection(direction);

                    if (newModel != null)
                    {
                        if (_selectedInteractable != null)
                            _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;
                    
                        _selectedInteractable = newModel;
                        _selectedInteractable.AbstractInteractableModel.IsSelected.Value = true;
                    }
                }
            }
            else if (direction.y != 0)
            {
                if (_selectedInteractable != null)
                {
                    if (_selectedInteractable.CustomBehaviour is NeedleBehaviour needleBehaviour)
                    {
                        needleBehaviour.SetDirection((int)direction.y);
                        needleBehaviour.Interact(null);
                    }
                }
            }
        }
    }
}