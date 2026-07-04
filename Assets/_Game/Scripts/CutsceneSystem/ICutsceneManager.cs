using System;

namespace _Game.Scripts.CutsceneSystem
{
    public interface ICutsceneManager
    {
        void Play(ACutscene cutscene, Action onComplete = null);
    }
}