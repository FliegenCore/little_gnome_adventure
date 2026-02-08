using _Game.Scripts.InteractionSystems.Interactables.Items.Hints;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItemView : NightstandView
    {
        [field: SerializeField] public AbstractHintSelect AbstractHintSelect { get; private set; }
    }
}