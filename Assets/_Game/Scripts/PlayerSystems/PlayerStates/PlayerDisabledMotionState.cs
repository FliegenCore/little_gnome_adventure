using _Game.Scripts.CameraSystem;
using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerDisabledMotionState : PlayerState
    {
        private readonly CameraController _cameraController;
        
        public PlayerDisabledMotionState(Fsm fsm, PlayerModel playerModel, CameraController cameraController) : base(fsm, playerModel)
        {
            _cameraController = cameraController;
        }
        
        public override void Enter()
        {
            base.Enter();
            _cameraController.SetFollowTarget(null);
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}