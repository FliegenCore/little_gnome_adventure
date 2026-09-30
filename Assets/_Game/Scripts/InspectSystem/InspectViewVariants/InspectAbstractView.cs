using _Game.Scripts.PlayerSystems.Animations;
using Assets._Game.Scripts.InspectSystem.Impl;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants
{
    public abstract class InspectAbstractView : MonoBehaviour
    {
        [field: SerializeField] public Activator Activator { get; private set; }
        [field: SerializeField] public Transform OpenTransform { get; private set; }
        [field: SerializeField] public InspectActivator InspectActivator { get; private set; }
    }
}