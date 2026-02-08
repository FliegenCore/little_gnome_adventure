using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems;
using Core.Common;

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
            RegisterTable();
        }

        private void RegisterNightstand()
        {
            InspectsView inspectsView = _forestRootViewFactory.GetLocationsRootView().InspectsView;

            BaseItemView toyView = inspectsView.InspectNightstandView.Toy;
            BaseItemView appleView = inspectsView.InspectNightstandView.Apple;
            
            BaseItem toy = CreateInteractableItem(ItemId.Toy, toyView, true);
            BaseItem apple = CreateInteractableItem(ItemId.Apple, appleView, true);
            
            RegisterInspect("Nightstand", inspectsView.InspectNightstandView, toy, apple);
        }

        private void RegisterTable()
        {
            InspectsView inspectsView = _forestRootViewFactory.GetLocationsRootView().InspectsView;
            RegisterInspect("Table", inspectsView.Table);
        }

        private void RegisterInspect(string id, InspectAbstractView view, params AbstractInteractable[] interactables)
        {
            InspectModel inspectModel = new InspectModel(interactables);
            
            view.Activator.Construct(inspectModel.IsOpen);
            
            _inspectController.AddInspectModel(id, inspectModel);
        }

        private BaseItem CreateInteractableItem(ItemId id, BaseItemView view, bool isEnabled)
        {
            BaseItemModel model = new BaseItemModel(view.ContactTriggerProvider, view.transform.position, id.ToString(), isEnabled);
            view.AbstractHintSelect.Construct(_eventBus, model.IsSelected);
            
            BaseItem baseItem = new BaseItem(model, view, _eventBus, _inventory);

            return baseItem;
        }
    }
}