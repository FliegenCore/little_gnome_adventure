using System.Collections.Generic;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.Quests.LobotomyQuest.Impl.Needle;
using _Game.Scripts.RoomSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl
{
    public class LobotomyManager
    {
        private readonly LocationsRootView _locationsRootView;
        private readonly IInteractableFactory _interactableFactory;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        private List<Interactable> _needles;
        
        public LobotomyManager(
            LocationsRootView locationsRootView, 
            IInteractableFactory interactableFactory, 
            InputSystem_Actions inputSystemActions, 
            EventBus eventBus,
            InspectRegistratorService inspectRegistratorService
            )
        {
            _inspectRegistratorService = inspectRegistratorService;
            _inputSystemActions        = inputSystemActions;
            _eventBus                  = eventBus;
            _interactableFactory       = interactableFactory;
            _locationsRootView         = locationsRootView;
        }

        public void Initialize()
        {
            CreateNeedles();
            RegisterLobotomy();
            RegisterInstructionPaper();
        }

        private void CreateNeedles()
        {
            _needles = new List<Interactable>();

            for (int i = 0; i < 3; i++)
            {
                NeedleModel needleModel = new NeedleModel(null, new Vector2(i, 0), "needle" + i);
                
                NeedleBehaviour needleBehaviour = new NeedleBehaviour(
                    _eventBus,
                    needleModel,
                    _locationsRootView.InspectsView.LobotomyInspectView.AnimationControl,
                    i);
                
                Interactable interactable = _interactableFactory.CreateInteractable(needleBehaviour, null, needleModel);
                needleBehaviour.Initialize();
                _needles.Add(interactable);
            }
        }
        
        private void RegisterLobotomy()
        {
            InspectsView inspectsView = _locationsRootView.InspectsView;
            _inspectRegistratorService.RegisterInspect("Lobotomy", inspectsView.LobotomyInspectView,true,
                new LobotomyInspectInput(_inputSystemActions, _eventBus),_needles.ToArray());
        }

        private void RegisterInstructionPaper()
        {
            InspectsView inspectsView = _locationsRootView.InspectsView;
            _inspectRegistratorService.RegisterInspect("Instruction", inspectsView.InstructionPaper);
        }
    }
}