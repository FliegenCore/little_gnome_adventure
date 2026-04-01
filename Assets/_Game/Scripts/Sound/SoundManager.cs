using Core.Common;
using UnityEngine;

namespace _Game.Scripts.Sound
{
    public class SoundManager : ISoundManager
    {
        private readonly AudioStorageConfig _audioStorageConfig;
        private readonly AudioSourceStorage _audioSourceStorage;
        private readonly EventBus _eventBus;
        
        
        private SoundManager(EventBus eventBus, AudioStorageConfig audioStorageConfig, AudioSourceStorage audioSourceStorage)
        {
            _audioSourceStorage = audioSourceStorage;
            _audioStorageConfig =  audioStorageConfig;
            _eventBus           = eventBus;
        }
        
        public void FadeBackgroundSoundWithNext(string audioClip)
        {
            AudioClip clip = _audioStorageConfig.AudioClips[audioClip];
            _audioSourceStorage.PlayBackgroundSound(clip);
        }

        public void PlayBackground(string audioClipName)
        {
        }

        public void PlayOnPosition(Vector2 position, float radius, string audioClip, bool isLoop)
        {
        }
    }
}