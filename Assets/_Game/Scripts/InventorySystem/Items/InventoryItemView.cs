using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItemView : NightstandView
    {
        [field: SerializeField] public SpriteApplyer SpriteApplyer { get; private set; }
    }
}