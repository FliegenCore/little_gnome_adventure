using System.Collections.Generic;
using System.Collections.ObjectModel;
using _Game.Scripts.Utils;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryModel
    {
        public readonly ObservableCollection<InventoryItemModel> ItemModels;
        
        public InventoryModel(List<InventoryItemModel> itemModels)
        {
            ItemModels = new ObservableCollection<InventoryItemModel>(itemModels);
        }
    }
}