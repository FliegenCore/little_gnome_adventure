using System;
using _Game.Scripts.FSM;
using _Game.Scripts.Input;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using Game.PlayerSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Game.Scripts.PlayerSystems
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IMoveDirectionInput _moveDirectionInput;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        private readonly InspectController _inspectController;

        private Player _player;
        
        public PlayerFactory(
            PlayerConfig playerConfig, 
            IMoveDirectionInput moveDirectionInput, 
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions,
            InspectController inspectController)
        {
            _inspectController = inspectController;
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _moveDirectionInput = moveDirectionInput;
            _playerConfig = playerConfig;
        }
        
        public Player CreatePlayer()
        {
            Transformation transformation = null;
           
            //if no data
            transformation = new Transformation(_playerConfig.StartSpawnPosition, _playerConfig.StartScale);
            //else load position
            
            AnimationPlayerModel animationPlayerModel = new AnimationPlayerModel();
            
            PlayerModel playerModel = new PlayerModel(transformation, _moveDirectionInput, animationPlayerModel, _playerConfig.MoveSpeed);
            
            PlayerView playerView = Object.Instantiate(_playerConfig.PlayerViewPrefab);
            playerView.Transformable.Construct(transformation);
            playerView.AnimationPlayer.Construct(playerModel.AnimationPlayerModel);
            
            Fsm motionFsm = new Fsm();
            FillPlayerMotion(motionFsm, playerModel);
            
            Fsm playerStateMachine = CreatePlayerStateMachine(playerModel);

            InteractionController interactionController = new InteractionController(_inputSystemActions, playerModel, _eventBus);
            
            Player player = new Player(playerModel, playerView, motionFsm, playerStateMachine, interactionController, _eventBus);
            _player = player;
            
            return player;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        private Fsm CreatePlayerStateMachine(PlayerModel model)
        {
            Fsm playerFsm = new Fsm();
            
            playerFsm.AddState(new PlayerBaseState(playerFsm, model));
            playerFsm.AddState(new PlayerInventoryState(playerFsm, model));
            playerFsm.AddState(new PlayerInspectState(playerFsm, model, _inspectController));
     
            playerFsm.SetState<PlayerBaseState>();
            
            return playerFsm;
        }
        
        private void FillPlayerMotion(Fsm fsm, PlayerModel model)
        {
            fsm.AddState(new PlayerIdleMotionState(fsm, model));
            fsm.AddState(new PlayerMoveMotionState(fsm, model));
            
            fsm.SetState<PlayerIdleMotionState>();
        }
    }
}