using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Scripts.Sound
{
    [CreateAssetMenu(fileName = nameof(AudioStorageConfig), menuName = "Hell/" + nameof(AudioStorageConfig))]
    public class AudioStorageConfig : ScriptableObject
    {
        public AudioMixerGroup AudioMixerGroup;

        [SerializeField] private string Path;
        
        [SerializeField] private AudioSource _oneShotAudioSourcePrefab;
        
        [SerializeField] private List<AudioClip> _audioClips;
        
        public List<AudioClip> AudioClips => _audioClips;
        public AudioSource OneShotAudioSourcePrefab => _oneShotAudioSourcePrefab;
        
        public void LoadAllAudioClips()
        {
            _audioClips = Resources.LoadAll<AudioClip>(Path).ToList();
            
            List<string> duplicateNames = _audioClips.GroupBy(u => u.name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            foreach (var duplicate in duplicateNames)
            {
                Debug.Log(duplicate);
            }
        }
        
        
    }
}