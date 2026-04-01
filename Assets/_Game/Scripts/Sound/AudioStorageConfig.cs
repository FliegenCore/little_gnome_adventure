using UnityEngine;

namespace _Game.Scripts.Sound
{
    [CreateAssetMenu(fileName = nameof(AudioStorageConfig), menuName = "Hell/" + nameof(AudioStorageConfig))]
    public class AudioStorageConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string,AudioClip> _audioClips;
        
        public SerializableDictionary<string, AudioClip> AudioClips => _audioClips;
    }
}