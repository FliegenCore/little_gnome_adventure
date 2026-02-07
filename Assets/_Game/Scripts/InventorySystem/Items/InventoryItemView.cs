using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItemView : MonoBehaviour
    {
        [field: SerializeField] public SpriteApplyer SpriteApplyer { get; private set; }
    }
}