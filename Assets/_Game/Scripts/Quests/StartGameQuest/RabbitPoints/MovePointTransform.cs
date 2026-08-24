using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class MovePointTransform : MonoBehaviour
    {
        [SerializeField] private MovePointTransform _nextPoint;
        
        public bool TryGetNextPoint(out MovePointTransform movePointTransform)
        {
            Debug.Log(_nextPoint);
            movePointTransform = _nextPoint;
            
            if (_nextPoint == null)
            {
                return false;
            }
            
            return true;
        }
    }
}