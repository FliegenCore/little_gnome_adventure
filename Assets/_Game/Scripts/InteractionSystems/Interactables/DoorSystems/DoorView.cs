using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class DoorView : NightstandView
    {
        [field: SerializeField] public DoorsIdEnum MarkId { get; private set; }
    }
}