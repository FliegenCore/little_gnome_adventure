using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class DoorView : MonoBehaviour
    {
        [field: SerializeField] public DoorsIdEnum MarkId { get; private set; }
        [field: SerializeField] public ContactTriggerProvider ContactTriggerProvider { get; private set; }
        public Vector2 Position => transform.position;
    }
}