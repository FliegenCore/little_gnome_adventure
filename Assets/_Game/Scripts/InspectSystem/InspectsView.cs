using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class InspectsView : MonoBehaviour
    {
        [field: SerializeField] public InspectNightstandView InspectNightstandView { get; private set; }
        [field: SerializeField] public InspectTableView Table { get; private set; }
    }
}