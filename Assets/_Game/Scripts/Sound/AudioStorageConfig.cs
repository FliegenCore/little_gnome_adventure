using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Scripts.Sound
{
    [CreateAssetMenu(fileName = nameof(AudioStorageConfig), menuName = "Hell/" + nameof(AudioStorageConfig))]
    public class AudioStorageConfig : ScriptableObject
    {
        public AudioMixerGroup AudioMixerGroup; 
        
        [SerializeField] private AudioSource _oneShotAudioSourcePrefab;
        
        [SerializeField] private List<AudioClip> _audioClips;
        
        public List<AudioClip> AudioClips => _audioClips;
        public AudioSource OneShotAudioSourcePrefab => _oneShotAudioSourcePrefab;
    }
}