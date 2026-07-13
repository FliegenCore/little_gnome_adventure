using _Game.Scripts.FSM;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.States;
using _Game.Scripts.UpdateSystems;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHand : IUpdateListener
    {
        public readonly GnomeHandModel HandModel;
        private readonly GnomeHandView _handView;
        
        private readonly Fsm _moveHandFsm;
        
        public GnomeHand(GnomeHandModel gnomeHandModel, GnomeHandView gnomeHandView, Fsm moveHandFsm)
        {
            HandModel    =  gnomeHandModel;
            _handView    = gnomeHandView;
            _moveHandFsm = moveHandFsm;
        }

        public void Update(float deltaTime)
        {
            _moveHandFsm?.Update(deltaTime);
            
            Debug.Log(_moveHandFsm?.CurrentState.ToString());
        }

        public void SetState<T>() where T : GnomeHandState
        {
            _moveHandFsm.SetState<T>();
        }
    }
}