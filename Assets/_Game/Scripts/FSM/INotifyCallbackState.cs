using System;

namespace _Game.Scripts.FSM
{
    public interface INotifyCallbackState
    {
        void SetCallback(Action callback);
    }
}