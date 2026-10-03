using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class PitCutscene : ACutscene
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly EventBus _eventBus;
        
        public PitCutscene(EventBus eventBus,IPlayerFactory playerFactory)
        {
            _eventBus      = eventBus;
            _playerFactory = playerFactory;
        }
        
        public override void Play(Action onComplete)
        {
            Player player = _playerFactory.GetPlayer();

            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerNoneState));
            player.SetPlayerMotionState(typeof(PlayerEmptyMotionState));
            
            player.PlayerView.AnimationPlayer.AnimationControl.SetAnimation(0, PlayerAnimationsName.PIT_ANIMATION_NAME, false,
                () =>
                {
                    onComplete?.Invoke();
                    _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
                    player.SetPlayerMotionState(typeof(PlayerIdleMotionState));
                });
        }
    }
}