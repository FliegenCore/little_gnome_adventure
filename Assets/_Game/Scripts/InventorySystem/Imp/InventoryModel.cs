using System.Collections.Generic;
using System.Collections.ObjectModel;
using _Game.Scripts.Utils;
using UniRx;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryModel
    {
        public readonly ObservableCollection<InventoryItemModel> ItemModels;
        public readonly ReactiveProperty<bool> IsOpen;
        public readonly ReactiveProperty<int> SelectedIndex;
        public readonly ReactiveCollection<InventoryItem> InventoryItems;
        
        public InventoryModel(List<InventoryItemModel> itemModels)
        {
            InventoryItems = new ReactiveCollection<InventoryItem>();
            SelectedIndex = new  ReactiveProperty<int>(0);
            IsOpen = new ReactiveProperty<bool>();
            ItemModels = new ObservableCollection<InventoryItemModel>(itemModels);
        }
    }
}