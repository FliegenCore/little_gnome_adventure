using _Game.Scripts.InspectSystem;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectRegistratorService
    {
        private readonly InspectController _inspectController;
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly InventoryProxy _inventoryProxy;
        
        public InspectRegistratorService(
            InspectController inspectController, 
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            InventoryProxy inventoryProxy)
        {
            _inventoryProxy = inventoryProxy;
            _eventBus = eventBus;
            _inspectController = inspectController;
            _rootViewFactory = rootViewFactory;
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
            InspectsView inspectsView = _rootViewFactory.GetLocationsRootView().InspectsView;

            BaseItemView toyView = inspectsView.InspectNightstandView.Toy;
            BaseItemView appleView = inspectsView.InspectNightstandView.Apple;
            
            BaseItem toy = CreateInteractableItem(ItemId.Toy, toyView, true);
            BaseItem apple = CreateInteractableItem(ItemId.Apple, appleView, true);
            
            RegisterInspect("Nightstand", inspectsView.InspectNightstandView, true, null, toy, apple);
        }

        private void RegisterTable()
        {
            InspectsView inspectsView = _rootViewFactory.GetLocationsRootView().InspectsView;
            RegisterInspect("Table", inspectsView.Table);
        }

        public void RegisterInspect(string id, InspectAbstractView view, bool isClosable = true, InspectInputHandler inspectInputHandler = null, params AbstractInteractable[] interactables)
        {
            InspectModel inspectModel = new InspectModel(isClosable, interactables);
            
            if(view != null)
                view.Activator.Construct(inspectModel.IsOpen);
            
            _inspectController.AddInspectModel(id, inspectModel, inspectInputHandler);
        }

        public void RegisterNotClosableInspects(string id, InspectAbstractView view, InspectInputHandler inspectInputHandler = null, params AbstractInteractable[] interactables)
        {
            RegisterInspect(id, view, false, inspectInputHandler, interactables);
        }

        private BaseItem CreateInteractableItem(ItemId id, BaseItemView view, bool isEnabled)
        {
            BaseItemModel model = new BaseItemModel(view.ContactTriggerProvider, view.transform.position, id.ToString(), isEnabled);
            view.HintSelect.Construct(_eventBus, model.IsSelected);
            
            BaseItem baseItem = new BaseItem(model, view, _eventBus, _inventoryProxy, null);

            return baseItem;
        }
    }
}