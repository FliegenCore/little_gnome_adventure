using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.Quests.ClanDoorQuest.Gates;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class InspectsView : MonoBehaviour
    {
        [field: SerializeField] public InspectNightstandView InspectNightstandView { get; private set; }
        [field: SerializeField] public InspectTableView Table { get; private set; }
        [field: SerializeField] public LobotomyInspectView LobotomyInspectView { get; private set; }
        [field: SerializeField] public InspectEmptyView InstructionPaper { get; private set; }
        [field: SerializeField] public InteractableAnimationView BusJumpInteractableAnimation { get; private set; }
        [field: SerializeField] public HellGatesPasswordInspectView GatesPassword { get; private set; }
    }
}