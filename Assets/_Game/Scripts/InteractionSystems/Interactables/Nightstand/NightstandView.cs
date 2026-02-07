using System;
using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View
{
    public class NightstandView : MonoBehaviour
    {
        [field: SerializeField] public ContactTriggerProvider ContactTriggerProvider { get; private set; }
        public Vector2 Position => transform.position;
    }
}