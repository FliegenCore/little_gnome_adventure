using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class DoorView : NightstandView
    {
        [Header("Door settings")]
        [field: SerializeField] public DoorsIdEnum MarkId { get; private set; }
        [field: SerializeField] public Transform SpawnTransform { get; private set; }
    }
}