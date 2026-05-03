using _Game.Scripts.InspectSystem;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.Quests.LobotomyQuest.Impl.Needle;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl
{
    public class LobotomyInspectInput : InspectInputHandler
    {
        public LobotomyInspectInput(InputSystem_Actions inputSystemActions) : base(inputSystemActions)
        {
            
        }
        
        public override void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            if (_currentInspectModel.Interactables.Count <= 0)
                return;
            
            SelectFirst();
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
        }
        
        public override void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            
            if (_selectedInteractable != null)
                _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;
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