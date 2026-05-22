namespace _Game.Scripts.InventorySystem
{
    public class SelectedItemManager
    {
        public readonly SelectedItemView SelectedItemView;
        public readonly SelectedItemModel SelectedItemModel;
        
        public SelectedItemManager(SelectedItemView view, SelectedItemModel selectedModel)
        {
            SelectedItemView = view;
            SelectedItemModel = selectedModel;
            
            view.Construct(SelectedItemModel);
        }
    }
}