using _Game.Scripts.FSM;
using _Game.Scripts.Input;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.Animations;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.States;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.UpdateSystems;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHandFactory
    {
        private readonly UpdateController _updateController;
        private readonly RootViewFactory _rootViewFactory;
        private readonly InputSystem_Actions _inputSystemActions;

        private GnomeHand _cachedHand;
        
        public GnomeHandFactory(
            UpdateController updateController,
            RootViewFactory rootViewFactory,
            InputSystem_Actions inputSystemActions
        )
        {
            _inputSystemActions = inputSystemActions;
            _rootViewFactory    = rootViewFactory;
            _updateController   = updateController;
        }

        public GnomeHand Create(Vector2 position)
        {
            if (_cachedHand != null)
            {
                _cachedHand.HandModel.Transformation.Position.Value = position;
                
                return _cachedHand;
            }
            
            Transformation transformation = new Transformation(position, Vector3.one);
            MoveDirectionInput moveDirectionInput = new MoveDirectionInput(_inputSystemActions);
            GnomeHandView gnomeHandView = _rootViewFactory.GetLocationsRootView().RunnerLocationView.GnomeHandView;

            GnomeHandAnimationModel animationModel = new GnomeHandAnimationModel();
            
            gnomeHandView.GnomeHandAnimationView.Construct(animationModel);

            GroundChecker groundChecker = new GroundChecker(gnomeHandView.ContactColliderProvider, gnomeHandView.ContactTriggerProvider);
            
            GnomeHandModel gnomeHandModel = new GnomeHandModel(
                transformation,
                moveDirectionInput, 
                animationModel,
                groundChecker,
                5,
                gnomeHandView.JumpDuration,
                gnomeHandView.JumpHeight);
            
            gnomeHandView.Transformable.Construct(transformation);
            
            Fsm fsm = CreateFsm(gnomeHandModel);
            
            GnomeHand hand = new GnomeHand(
                gnomeHandModel,
                gnomeHandView,
                fsm);
            
            _updateController.AddListener(hand);
            
            _cachedHand = hand;
            
            return hand;
        }

        private Fsm CreateFsm(GnomeHandModel model)
        {
            Fsm fsm = new Fsm();
            
            fsm.AddState(new GnomeHandIdleState(fsm, model));
            fsm.AddState(new GnomeHandMoveState(fsm, model));
            fsm.AddState(new GnomeHandAirState(fsm, model));
            fsm.AddState(new GnomeHandJumpState(fsm, model));
            fsm.SetState<GnomeHandIdleState>();
            
            return fsm;
        }
    }
}