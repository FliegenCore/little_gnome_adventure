using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants
{
    public abstract class InspectAbstractView : MonoBehaviour
    {
        [field: SerializeField] public Activator Activator { get; private set; }
    }
}