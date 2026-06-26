using UnityEngine;

namespace _Game.Scripts.Sound
{
    public interface ISoundManager
    {
        void FadeBackgroundSoundWithNext(string audioClip);
        void PlayBackground(string audioClipName);
        void PlayOnPosition(Transform parent, float radius, string audioClip, bool isLoop);
        void PlayEffectOnBackground(Transform parent, string audioClip, bool isLoop);
        bool HasSound(string audioClipName);
    }
}