using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.InputInfoSystem
{
    [CreateAssetMenu(fileName = nameof(InputInfoConfig), menuName = GameGlobalPath.SoPath + nameof(InputInfoConfig))]
    public class InputInfoConfig : ScriptableObject
    {
        [SerializeField] private List<InputInfoSpriteStorage> _inputInfoSpriteStorage;

        private Dictionary<EKeyIndex, Sprite> _inputInfoSpriteStorageDict;
        
        public Sprite GetSprite(EKeyIndex index)
        {
            if (_inputInfoSpriteStorageDict == null)
            {
                CreateCache();
            }

            return _inputInfoSpriteStorageDict?[index];
        }

        private void CreateCache()
        {
            _inputInfoSpriteStorageDict = new Dictionary<EKeyIndex, Sprite>();
            
            foreach (var infoSpriteStorage in _inputInfoSpriteStorage)
            {
                _inputInfoSpriteStorageDict[infoSpriteStorage.KeyIndex] = infoSpriteStorage.Sprite;
            }
        }
    }
}