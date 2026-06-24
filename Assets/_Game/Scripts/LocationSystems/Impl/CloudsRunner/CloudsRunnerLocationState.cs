using System;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.MiniGames.CloudsRunner;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.CloudsRunner
{
    public class CloudsRunnerLocationState : LocationAbstractState
    {
        private readonly CloudsRunnerInitializer _cloudsRunnerInitializer;
        
        public CloudsRunnerLocationState(
            Fsm fsm, 
            CloudsRunnerLocationModel locationModel, 
            CloudsRunnerLocationView abstractLocation,
            IDialogueManager dialogueManager, 
            EventBus eventBus,
            CloudsRunnerInitializer cloudsRunnerInitializer
            ) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
            _cloudsRunnerInitializer = cloudsRunnerInitializer;
        }

        public override void Enter()
        {
            base.Enter();
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDisabledMotionState));
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            
            _cloudsRunnerInitializer.Initialize(); //временно тут
            //disable player camera set camera follow
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}