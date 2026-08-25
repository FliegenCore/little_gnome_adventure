using _Game.Scripts.Characters.Rabbit.OtherComponents;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;

namespace _Game.Scripts.Quests.StartGameQuest.RabbitBehaviours
{
    public class StartGameQuestPlayerTriggersInitializer
    {
        private readonly IContactTriggerProvider[] _playerTriggerProviders;
        private readonly IPlayerFactory _playerFactory;
        
        public StartGameQuestPlayerTriggersInitializer(
            IContactTriggerProvider[] playerTriggerProviders,
            IPlayerFactory playerFactory
            )
        {
            _playerFactory          = playerFactory;
            _playerTriggerProviders = playerTriggerProviders;
        }

        public void Initialize()
        {
            Player player = _playerFactory.GetPlayer();

            foreach (IContactTriggerProvider playerTriggerProvider in _playerTriggerProviders)
            {
                TriggerFsmStateEnabler triggerFsmStateEnabler = new TriggerFsmStateEnabler(
                    player.MotionStateMachine,
                    playerTriggerProvider, 
                    typeof(PlayerIdleMotionState), 
                    false, 
                    typeof(PlayerIdleMotionState),
                    false 
                );

                triggerFsmStateEnabler.PrepareSetState += SetSneakPlayerState;
                triggerFsmStateEnabler.PrepareSetExitState += SetBasePlayerState;
            }
        }

        private void SetSneakPlayerState()
        {
            Player player = _playerFactory.GetPlayer();

            player.PlayerModel.MotionOverridedStates.IdleType = typeof(PlayerIdleSneakMotionState);
        }

        private void SetBasePlayerState()
        {
            Player player = _playerFactory.GetPlayer();
            
            player.PlayerModel.MotionOverridedStates.IdleType = null;
        }
    }
}