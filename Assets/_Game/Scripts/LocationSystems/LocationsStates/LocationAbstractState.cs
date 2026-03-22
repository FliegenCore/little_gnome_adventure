using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;
using Core.Common;
using TMPro;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class LocationAbstractState : FsmAbstractState
    {
        public readonly AbstractLocationView AbstractLocationView;
        public readonly AbstractLocationModel LocationModel;

        protected readonly IDialogueManager _dialogueManager;
        protected readonly EventBus _eventBus;
        
        public LocationAbstractState(
            Fsm fsm, 
            AbstractLocationModel locationModel,
            AbstractLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm)
        {
            LocationModel = locationModel;
            _eventBus            = eventBus;
            _dialogueManager     = dialogueManager;
            AbstractLocationView = abstractLocation;
        }

        public override void Enter()
        {
            List<SpeakerView> speakerViews = new List<SpeakerView>();
            speakerViews.AddRange(AbstractLocationView.SpeakerViews);

            foreach (var speaker in speakerViews)
            {
                speaker.Initialize(_eventBus);
            }
            
            _dialogueManager.RegisterSpeakerCharacters(speakerViews.ToArray());
            
            AbstractLocationView.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            List<SpeakerView> speakerViews = new List<SpeakerView>();
            speakerViews.AddRange(AbstractLocationView.SpeakerViews);
            _dialogueManager.UnregisterSpeakerCharacters(speakerViews);
            
            AbstractLocationView.gameObject.SetActive(false);
        }

        public override void Update(float deltaTime)
        {
        }
    }
}