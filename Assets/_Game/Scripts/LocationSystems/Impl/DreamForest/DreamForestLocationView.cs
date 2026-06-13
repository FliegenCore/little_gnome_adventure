using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationView : AbstractLocationView
    {
        [field: SerializeField] public NightstandView McMushroomView { get; private set; }
        [field: SerializeField] public BaseItemView[] Mushrooms { get; private set; }
        [field: SerializeField] public BusmanView BusmanView { get; private set; }
    }
}