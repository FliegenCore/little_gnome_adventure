using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.Input;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using Game.PlayerSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.PlayerSystems
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerConfig _playerConfig;
        private readonly IMoveDirectionInput _moveDirectionInput;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        private readonly InspectController _inspectController;
        private readonly InventoryFactory _inventoryFactory;
        private readonly IDialogueManager _dialogueManager;
        private readonly IObjectResolver _resolver; 

        private Player _player;
        private Inventory _inventory;
        
        public PlayerFactory(
            PlayerConfig playerConfig, 
            IMoveDirectionInput moveDirectionInput, 
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions,
            InspectController inspectController,
            InventoryFactory inventoryFactory,
            IDialogueManager dialogueManager,
            IObjectResolver resolver) 
        {
            _dialogueManager    = dialogueManager;
            _inventoryFactory   = inventoryFactory;
            _inspectController  = inspectController;
            _inputSystemActions = inputSystemActions;
            _eventBus           = eventBus;
            _moveDirectionInput = moveDirectionInput;
            _playerConfig       = playerConfig;
            _resolver           = resolver; 
        }
        
        public Player CreatePlayer()
        {
            Transformation transformation = null;
           
            //if no data
            transformation = new Transformation(_playerConfig.StartSpawnPosition, _playerConfig.StartScale);
            //else load position
            
            AnimationPlayerModel animationPlayerModel = new AnimationPlayerModel();
            
            PlayerModel playerModel = new PlayerModel(transformation, _moveDirectionInput, animationPlayerModel, _playerConfig.MoveSpeed);
            
            PlayerView playerView = _resolver.Instantiate(_playerConfig.PlayerViewPrefab, _playerConfig.StartSpawnPosition, Quaternion.identity);
            playerView.Transformable.Construct(transformation);
            
            playerView.AnimationPlayer.Construct(playerModel.AnimationPlayerModel);
            playerView.SpeakerView.Initialize(_eventBus);
            _dialogueManager.RegisterSpeakerCharacters(playerView.SpeakerView);
            
            Fsm motionFsm = new Fsm();
            FillPlayerMotion(motionFsm, playerModel);
            
            InteractionController interactionController = new InteractionController(_inputSystemActions, playerModel, _eventBus);
            _inventory = _inventoryFactory.CreateInventory(interactionController);
            
            Fsm playerStateMachine = CreatePlayerStateMachine(playerModel);

            Player player = new Player(
                playerModel,
                playerView, 
                motionFsm, 
                playerStateMachine,
                interactionController, 
                _inventory,
                _eventBus);
            
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
            
            playerFsm.AddState(new PlayerBaseState(playerFsm, model, _inventory, _eventBus));
            playerFsm.AddState(new PlayerInventoryState(playerFsm, model, _inventory));
            playerFsm.AddState(new PlayerInspectState(playerFsm, model, _inspectController));
            playerFsm.AddState(new PlayerDisabledMotionState(playerFsm, model));
            playerFsm.AddState(new PlayerDialogueState(playerFsm, model, _dialogueManager));
            playerFsm.AddState(new PlayerAutoMoveState(playerFsm, model));
     
            playerFsm.SetState<PlayerBaseState>();
            
            return playerFsm;
        }
        
        private void FillPlayerMotion(Fsm fsm, PlayerModel model)
        {
            fsm.AddState(new PlayerIdleMotionState(fsm, model));
            fsm.AddState(new PlayerAutoMoveMotionState(fsm, model));
            fsm.AddState(new PlayerMoveMotionState(fsm, model));
            
            fsm.SetState<PlayerIdleMotionState>();
        }
    }
}