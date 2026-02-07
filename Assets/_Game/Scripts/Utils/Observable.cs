using System;

namespace _Game.Scripts.Utils
{
    public class Observable<T> : IReadOnlyObservable<T>
    {
        public event Action<T> OnChanged;
        
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }
        
        public T ValueWithoutInvoke
        {
            get => _value;
            set => _value = value;
        }
        
        public Observable()
        {
            
        }

        public Observable(T value)
        {
            _value = value;
        }
        
        public void Subscribe(Action<T> observer)
        {
            OnChanged += observer;
            OnChanged?.Invoke(_value);
        }

        public void Unsubscribe(Action<T> observer)
        {
            OnChanged -= observer;
        }
    }

    public interface IReadOnlyObservable<T>
    {
        public T Value { get; }
        
        void Subscribe(Action<T> observer);
        void Unsubscribe(Action<T> observer);
    }
}