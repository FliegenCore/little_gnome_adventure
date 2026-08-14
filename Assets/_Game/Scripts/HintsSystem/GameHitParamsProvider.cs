namespace _Game.Scripts.HintsSystem
{
    public class GameHitParamsProvider
    {
        public object[] GetParams(EHintType hintType)
        {
            switch (hintType)
            {
                case EHintType.OpenInventoryHint :
                    return GetInventoryParams();
                    default:
                        
                        
                    return null;
            }
        }

        private object[] GetInventoryParams()
        {
            return new object[] { "" };
        }

        private void GetInputParams()
        {
            
        }
    }
}