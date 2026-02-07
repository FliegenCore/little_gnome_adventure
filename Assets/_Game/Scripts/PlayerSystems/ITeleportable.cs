using _Game.Scripts.RoomSystems;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public interface ITeleportable
    {
        void Teleport(Vector2 position);
    }
}