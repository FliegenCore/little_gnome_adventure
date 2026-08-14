using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.HintsSystem
{
    [CreateAssetMenu(fileName = nameof(GameHintsConfig), menuName = GameGlobalPath.SoPath + nameof(GameHintsConfig))]
    public class GameHintsConfig : ScriptableObject
    {
        [SerializeField] private GameHint[] _gameHints;
            
        private Dictionary<EHintType, string> _gameHintsDict;
        
        private bool _cacheIsCreated;
        
        private void CreateCache()
        {
            if (_cacheIsCreated)
                return;
            
            _gameHintsDict = new Dictionary<EHintType, string>();

            foreach (var gameHint in _gameHints)
            {
                _gameHintsDict.Add(gameHint.HintType, gameHint.Text);
            }
            
            _cacheIsCreated = true;
        }

        public string GetText(EHintType hintType)
        {
            CreateCache();
            
            return _gameHintsDict[hintType];
        }
    }

    [System.Serializable]
    public class GameHint
    {
        [field: SerializeField] public EHintType HintType { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
    }
}