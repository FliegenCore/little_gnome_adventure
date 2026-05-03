using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.LobotomyQuest.Impl.Hedgehog;
using _Game.Scripts.RoomSystems;
using UnityEngine;

namespace _Game.Scripts.LocationSystems.LocationsView
{
    public class ForestLocationView : AbstractLocationView
    {
        [field: SerializeField] public NightstandView GirlView { get; private set; }
        [field: SerializeField] public HedgehogView HedgehogView { get; private set; }
        [field: SerializeField] public BaseItemView WrapperItemView { get; private set; }
        [field: SerializeField] public BaseItemView PooItemView { get; private set; }
    }
}