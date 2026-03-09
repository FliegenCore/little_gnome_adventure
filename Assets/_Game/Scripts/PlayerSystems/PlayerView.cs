using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.PlayerSystems.Animations;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Transformable Transformable { get; private set; }
        [field: SerializeField] public AnimationPlayer AnimationPlayer { get; private set; }
        [field: SerializeField] public SpeakerView SpeakerView { get; private set; }
    }
}