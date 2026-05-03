using System;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Interactables.Items.Hints;
using _Game.Scripts.InventorySystem;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View
{
    [RequireComponent(typeof(ContactTriggerProvider))]
    public class NightstandView : MonoBehaviour
    {
        [field: SerializeField] public Transform InteractPoint { get; private set; }
        [field: SerializeField] public BoxCollider2D BoxCollider2D { get; private set; }
        [field: SerializeField] public ContactTriggerProvider ContactTriggerProvider { get; private set; }
        [field: SerializeField] public AbstractHintSelect HintSelect { get; private set; }
        
        public Vector2 Position => transform.position;
    }
}