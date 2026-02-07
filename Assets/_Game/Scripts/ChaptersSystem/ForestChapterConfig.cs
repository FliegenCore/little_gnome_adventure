using System;
using _Game.Scripts.RoomSystems;
using UnityEngine;

namespace _Game.Scripts.GameInitializeSystems
{
    [CreateAssetMenu(fileName = "ForestChapterConfig", menuName = "Hell/Create ForestChapterConfig")]
    public class ForestChapterConfig : ScriptableObject
    {
        [field: SerializeField] public DoorConnections[] DoorConnections { get; private set; }
    }

    [Serializable]
    public class DoorConnections
    {
        [field: SerializeField] public DoorsIdEnum Id { get; private set; }
        [field: SerializeField] public DoorsIdEnum ConnectionId { get; private set; }
    }
}