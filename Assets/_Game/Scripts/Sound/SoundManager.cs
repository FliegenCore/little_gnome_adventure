using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.Sound
{
    public class SoundManager : ISoundManager, IDisposable
    {
        private readonly AudioStorageConfig _audioStorageConfig;
        private readonly AudioSourceStorage _audioSourceStorage;
        private readonly EventBus _eventBus;
        private Dictionary<string, AudioClip> _audioClipDictionary;
        
        private SoundManager(EventBus eventBus, AudioStorageConfig audioStorageConfig, AudioSourceStorage audioSourceStorage)
        {
            _audioSourceStorage  = audioSourceStorage;
            _audioStorageConfig  =  audioStorageConfig;
            _eventBus            = eventBus;
            _audioClipDictionary = _audioStorageConfig.AudioClips.ToDictionary(clip => clip.name, clip => clip);
            
            Initialize();
        }

        private void Initialize()
        {
            _eventBus.Subscribe<PlayOneShotSoundSignal, Transform, float, string, bool>(this, PlayOnPosition);
        }
        
        public void FadeBackgroundSoundWithNext(string audioClip)
        {
            AudioClip clip = _audioClipDictionary[audioClip];
            _audioSourceStorage.PlayBackgroundSound(clip);
        }

        public void PlayBackground(string audioClipName)
        {
        }
        
        public void PlayOnPosition(Transform parent, float radius, string audioClip, bool isLoop)
        {
            Debug.Log(audioClip);
            Vector3 worldPos = new Vector3(parent.position.x, parent.position.y, 0f);
            AudioSource audio = UnityEngine.Object.Instantiate(
                _audioStorageConfig.OneShotAudioSourcePrefab,
                worldPos, 
                Quaternion.identity,
                parent);
            audio.outputAudioMixerGroup = _audioStorageConfig.AudioMixerGroup;
            audio.clip = _audioClipDictionary[audioClip];
            audio.loop = isLoop;
    
            audio.spatialBlend = 1f;                 
            audio.rolloffMode = AudioRolloffMode.Linear;
            audio.minDistance = 0f;                  
            audio.maxDistance = radius;              
    
            audio.Play();
    
            if (!isLoop)
            {
                UnityEngine.Object.Destroy(audio.gameObject, audio.clip.length);
            }
        }

        public bool HasSound(string audioClipName)
        {
            return _audioClipDictionary.ContainsKey(audioClipName);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<PlayOneShotSoundSignal>(this);
        }
    }
}