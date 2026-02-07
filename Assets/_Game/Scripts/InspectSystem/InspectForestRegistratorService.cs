using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems;
using Core.Common;
using VContainer.Unity;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectForestRegistratorService
    {
        private readonly InspectController _inspectController;
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly Inventory _inventory;
        
        public InspectForestRegistratorService(InspectController inspectController, ForestRootViewFactory forestRootViewFactory, EventBus eventBus)
        {
            _eventBus = eventBus;
            _inspectController = inspectController;
            _forestRootViewFactory = forestRootViewFactory;
        }

        public void Initialize()
        {
            RegisterInspects();
        }
        
        private void RegisterInspects()
        {
            RegisterNightstand();
        }

        private void RegisterNightstand()
        {
            InspectsView inspectsView = _forestRootViewFactory.GetLocationsRootView().InspectsView;
            
            RegisterInspect("Nightstand", inspectsView.InspectNightstandView);
            RegisterInspect("Table", inspectsView.Table);
        }

        private void RegisterInspect(string id, InspectAbstractView view, params AbstractInteractable[] interactables)
        {
            InspectModel inspectModel = new InspectModel(interactables);
            
            view.Activator.Construct(inspectModel.IsOpen);
            
            _inspectController.AddInspectModel(id, inspectModel);
        }

        private BaseItem CreateInteractableItem(string id, IContactTriggerProvider triggerProvider, NightstandView view)
        {
            BaseItemModel model = new BaseItemModel(triggerProvider, view.transform.position, id);
            BaseItem baseItem = new BaseItem(model, view, _eventBus, _inventory);

            return baseItem;
        }
    }
}