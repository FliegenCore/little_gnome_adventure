using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems;
using VContainer.Unity;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectForestRegistratorService
    {
        private readonly InspectController _inspectController;
        private readonly ForestRootViewFactory _forestRootViewFactory;
        
        public InspectForestRegistratorService(InspectController inspectController, ForestRootViewFactory forestRootViewFactory)
        {
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
    }
}