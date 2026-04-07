using System;

namespace _Game.Scripts.CutsceneSystem
{
    public interface ICutsceneManger
    {
        void Play(ACutscene cutscene, Action onComplete = null);
    }
}