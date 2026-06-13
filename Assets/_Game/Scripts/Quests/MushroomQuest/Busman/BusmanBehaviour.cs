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

        private readonly ICutsceneManger _cutsceneManger;

        private readonly ACutscene _cutscene;
        
        public BusmanBehaviour(EventBus eventBus, Fsm fsm, ICutsceneManger cutsceneManger, ACutscene cutscene) : base(eventBus)
        {
            _cutsceneManger = cutsceneManger;
            _busmanMachine  = fsm;
            _cutscene       = cutscene;
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            _cutsceneManger.Play(_cutscene);
        }
    }
}