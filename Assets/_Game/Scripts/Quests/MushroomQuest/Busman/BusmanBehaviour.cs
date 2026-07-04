using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using Core.Common;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanBehaviour : ACustomBehaviour
    {
        private readonly Fsm _busmanMachine;

        private readonly ICutsceneManager _cutsceneManager;

        private readonly ACutscene _cutscene;
        
        public BusmanBehaviour(EventBus eventBus, Fsm fsm, ICutsceneManager cutsceneManager, ACutscene cutscene) : base(eventBus)
        {
            _cutsceneManager = cutsceneManager;
            _busmanMachine  = fsm;
            _cutscene       = cutscene;
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            _cutsceneManager.Play(_cutscene);
        }
    }
}