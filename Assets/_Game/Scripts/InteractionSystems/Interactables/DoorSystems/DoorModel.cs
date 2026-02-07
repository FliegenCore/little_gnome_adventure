using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class DoorModel : AbstractInteractableModel
    {
        public readonly string Id;
        public readonly string ConnectedDoorId;

        public bool IsOpen = true;
        
        public DoorModel(IContactTriggerProvider contactTriggerProvider, string id, Vector2 position, string connectedDoorId) 
            : base(contactTriggerProvider, position, id)
        {
            Id = id;
            ConnectedDoorId = connectedDoorId;
        }
    }
}