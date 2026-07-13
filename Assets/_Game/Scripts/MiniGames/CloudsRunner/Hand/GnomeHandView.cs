using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InteractionSystems.Imp;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHandView : MonoBehaviour
    {
        [field: SerializeField] public ContactTriggerProvider ContactTriggerProvider { get; private set; }
        [field: SerializeField] public ContactColliderProvider ContactColliderProvider { get; private set; }
        [field: SerializeField] public Activator Activator { get; private set; }
        [field: SerializeField] public Transformable Transformable { get; private set; }
        [field: SerializeField] public GnomeHandAnimationView GnomeHandAnimationView { get; private set; }
        
        
        [field: SerializeField] public float JumpDuration { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
    }
}