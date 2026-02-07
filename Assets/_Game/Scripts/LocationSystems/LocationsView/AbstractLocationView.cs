using System;
using _Game.Scripts.UpdateSystems;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public abstract class AbstractLocationView : MonoBehaviour
    {
        [field: SerializeField] public LocationsIdEnum LocationsId { get; private set; }
    }
}