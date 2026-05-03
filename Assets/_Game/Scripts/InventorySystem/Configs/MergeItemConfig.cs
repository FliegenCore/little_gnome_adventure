using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.InventorySystem.Configs
{
    [CreateAssetMenu(fileName = "MergeItemConfig", menuName = "Hell/MergeItemConfig")]
    public class MergeItemConfig : ScriptableObject
    {
        [SerializeField] private List<MergeItemInfo> _items;

        public bool HasMerge(ItemId firstItem, ItemId secondItem, out ItemId resultItem)
        {
            resultItem = ItemId.Apple;
            
            foreach (var mergeItemInfo in _items)
            {
                if (mergeItemInfo.FirstItem == firstItem && mergeItemInfo.SecondItem == secondItem || 
                    mergeItemInfo.FirstItem == secondItem && mergeItemInfo.SecondItem == firstItem)
                {
                    resultItem = mergeItemInfo.ResultItem;
                    return true;
                }
            }

            return false;
        }
    }

    [System.Serializable]
    public class MergeItemInfo
    {
        public ItemId FirstItem;
        public ItemId SecondItem;
        
        public ItemId ResultItem;
    }
}