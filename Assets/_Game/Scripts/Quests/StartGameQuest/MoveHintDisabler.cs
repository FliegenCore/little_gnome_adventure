using System;
using _Game.Scripts.HintsSystem;
using Core.Common;
using UniRx;
using VContainer.Unity;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class MoveHintDisabler : IInitializable
    {
        private readonly PlayerMoveListener _playerMoveListener;
        private readonly EventBus _eventBus;

        private MoveHintDisabler(PlayerMoveListener playerMoveListener, EventBus eventBus)
        {
            _playerMoveListener = playerMoveListener;
            _eventBus           = eventBus;
        }

        public void Initialize()
        {
            _playerMoveListener.OnPlayerMoved += DisableMoveHint;
        }
        
        private void DisableMoveHint()
        {
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                _eventBus.TriggerEvenet<HideGameHintSignal>();
            });
        }
    }
}