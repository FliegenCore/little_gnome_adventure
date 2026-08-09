namespace _Game.Scripts.RoomSystems.InputInfoSystem
{
    public struct InputInfoGroup
    {
        public readonly EKeyIndex[] KeyIndices;
        public readonly string Description;
        
        public InputInfoGroup(string description, params EKeyIndex[] inputInfos)
        {
            KeyIndices  = inputInfos;
            Description = description;
        }
    }
}