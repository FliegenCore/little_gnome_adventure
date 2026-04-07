using System;

namespace _Game.Scripts.CutsceneSystem
{
    public abstract class ACutscene
    {
        public abstract void Play(Action onComplete);
    }
}