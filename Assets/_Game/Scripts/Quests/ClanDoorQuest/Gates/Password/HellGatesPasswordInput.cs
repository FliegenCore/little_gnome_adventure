using System;
using _Game.Scripts.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems.InputInfoSystem;
using Core.Common;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordInput : InspectInputHandler
    {
        private readonly HellGatesPasswordModel _hellGatesPasswordModel;
        private CompositeDisposable _disposables = new CompositeDisposable();

        public HellGatesPasswordInput(
            InputSystem_Actions inputSystemActions,
            EventBus eventBus,
            HellGatesPasswordModel hellGatesPasswordModel
            ) : base(inputSystemActions, eventBus)
        {
            _hellGatesPasswordModel = hellGatesPasswordModel;
        }

        public override void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            InputInfoGroup moveGroup =
                new InputInfoGroup("перемещение", EKeyIndex.W, EKeyIndex.A, EKeyIndex.S, EKeyIndex.D);
            
            InputInfoGroup escapeGroup =
                new InputInfoGroup("выход", EKeyIndex.Esc);
            
            InputInfoGroup eGroup =
                new InputInfoGroup("нажать кнопку", EKeyIndex.E);
            
            _eventBus.TriggerEvenet<ShowInputInfoViewSignal, InputInfoGroup[]>(new[] {escapeGroup, moveGroup, eGroup});
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
            _inputSystemActions.Player.Interact.performed += Interact;
        }

        public override void DisableInput()
        {
            _eventBus.TriggerEvenet<HideInputInfoViewSignal>();
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _inputSystemActions.Player.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext callback)
        {
            if (!_hellGatesPasswordModel.CanWrite || _hellGatesPasswordModel.PublicWriteLock)
                return;
            
            string index = _hellGatesPasswordModel.CurrentIndex.Value.ToString();

            _hellGatesPasswordModel.CurrentPassword.Value += index;
            _hellGatesPasswordModel.PressButton.OnNext(Unit.Default);
            _hellGatesPasswordModel.WritedCount.Value++;
            
            _hellGatesPasswordModel.CanWrite = false;
        }

        protected override void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();
            
            int index = _hellGatesPasswordModel.CurrentIndex.Value;
            
            if (direction.x != 0)
            {
                if (direction.x > 0)
                {
                    SetCurrentIndex(index + 1);
                }
                else
                {
                    SetCurrentIndex(index - 1);
                }
            }
            else if (direction.y != 0)
            {
                if (direction.y < 0)
                {
                    SetCurrentIndex(index + 3);
                }
                else
                {
                    SetCurrentIndex(index - 3);
                }
            }
        }

        private void SetCurrentIndex(int index)
        {
            if (index < 0)
                return;
            if (index > 8)
                return;
                
            _hellGatesPasswordModel.CurrentIndex.Value = index;
        }
    }
}