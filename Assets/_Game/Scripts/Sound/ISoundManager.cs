using UnityEngine;

namespace _Game.Scripts.Sound
{
    public interface ISoundManager
    {
        void FadeBackgroundSoundWithNext(string audioClip);
        void PlayBackground(string audioClipName);
        void PlayOnPosition(Vector2 position, float radius, string audioClip, bool isLoop);
    }
}