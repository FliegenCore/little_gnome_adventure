using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class LocationAbstractState : FsmAbstractState
    {
        public readonly AbstractLocationView AbstractLocationView;

        protected readonly IDialogueManager _dialogueManager;
        
        public LocationAbstractState(
            Fsm fsm, 
            AbstractLocationView abstractLocation,
            IDialogueManager dialogueManager) : base(fsm)
        {
            _dialogueManager     = dialogueManager;
            AbstractLocationView = abstractLocation;
        }

        public override void Enter()
        {
            List<SpeakerView> speakerViews = new List<SpeakerView>();
            speakerViews.AddRange(AbstractLocationView.SpeakerViews);
            
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