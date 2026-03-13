using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems;
using UnityEngine;

namespace _Game.Scripts.LocationSystems.LocationsView
{
    public class ForestLocationView : AbstractLocationView
    {
        [field: SerializeField] public NightstandView GirlView { get; private set; }
    }
}