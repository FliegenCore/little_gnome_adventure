using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows
{
    public class Nightstand : AbstractInteractable
    {
        private readonly NightstandModel _nightstandModel;
        private readonly NightstandView _nightstandView;
        
        public Nightstand(EventBus eventBus, NightstandModel nightstandModel, NightstandView nightstandView) : base(nightstandModel, eventBus)
        {
            _nightstandModel = nightstandModel;
            _nightstandView = nightstandView;
        }

        public override void Interact()
        {
            EventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>(_nightstandModel.Id);
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}