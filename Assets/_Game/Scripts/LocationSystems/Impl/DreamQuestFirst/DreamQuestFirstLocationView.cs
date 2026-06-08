using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.PlantsQuest;
using _Game.Scripts.Quests.PlantsQuest.Impl.Plant;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Impl.DreamQuestFirst
{
    public class DreamQuestFirstLocationView : AbstractLocationView
    {
        [field: SerializeField] public NightstandView GrannyView { get; private set; }
        [field: SerializeField] public PlantView CactusPlant { get; private set; }
        [field: SerializeField] public PlantView BootPlant { get; private set; }
        [field: SerializeField] public PlantView ColumnPlant { get; private set; }
        [field: SerializeField] public FocusTrigger FocusTrigger { get; private set; }
    }
}