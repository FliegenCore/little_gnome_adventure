using System;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.UpdateSystems;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public abstract class AbstractLocationView : MonoBehaviour
    {
        [field: SerializeField] public PolygonCollider2D CameraCollider { get; private set; }
        [field: SerializeField] public DoorView[] Doors { get; private set; }
        [field: SerializeField] public SpeakerView[] SpeakerViews { get; private set; }
    }
}