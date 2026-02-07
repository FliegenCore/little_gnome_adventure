using System;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public enum ItemId
    {
        Toy = 0,
        Apple = 1,
    }
    
    [CreateAssetMenu(fileName = "InventoryItemConfig", menuName = "Hell/Inventory Item Config")]
    public class ItemConfigs : ScriptableObject
    {
        [field: SerializeField] public ItemConfig[] Items { get; private set; } 
    }

    [Serializable]
    public struct ItemConfig
    {
        [field: SerializeField] public ItemId ItemId { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public InventoryItemView ViewPrefab { get; private set; }
    }
}