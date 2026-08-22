using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Common
{
    public class EventBus
    {
        // Тип сигнала -> (слушатель -> список обёрток)
        private readonly Dictionary<Type, Dictionary<object, List<Action<object[]>>>> _subscriptions;

        public EventBus()
        {
            _subscriptions = new Dictionary<Type, Dictionary<object, List<Action<object[]>>>>();
        }

        // ------------------ Вспомогательные методы ------------------

        private bool CheckAndAddListener<T>(object listener, Action<object[]> wrapper)
        {
            Type type = typeof(T);
            if (!_subscriptions.TryGetValue(type, out var listenerMap))
            {
                listenerMap = new Dictionary<object, List<Action<object[]>>>();
                _subscriptions[type] = listenerMap;
            }

            if (!listenerMap.TryGetValue(listener, out var wrappers))
            {
                wrappers = new List<Action<object[]>>();
                listenerMap[listener] = wrappers;
            }
         
            wrappers.Add(wrapper);
            return true;
        }

        public void Subscribe<T>(object listener, Action action) where T : class
        {
            Action<object[]> wrapper = args => action();
            CheckAndAddListener<T>(listener, wrapper);
        }

        public void Subscribe<T, U>(object listener, Action<U> action) where T : class
        {
            Action<object[]> wrapper = args =>
            {
                U arg = args.Length > 0 ? (U)args[0] : default(U);
                action(arg);
            };
            CheckAndAddListener<T>(listener, wrapper);
        }

        public void Subscribe<T, U, Q>(object listener, Action<U, Q> action) where T : class
        {
            Action<object[]> wrapper = args =>
            {
                U arg1 = args.Length > 0 ? (U)args[0] : default(U);
                Q arg2 = args.Length > 1 ? (Q)args[1] : default(Q);
                action(arg1, arg2);
            };
            CheckAndAddListener<T>(listener, wrapper);
        }

        public void Subscribe<T, U, Q, E>(object listener, Action<U, Q, E> action) where T : class
        {
            Action<object[]> wrapper = args =>
            {
                U arg1 = args.Length > 0 ? (U)args[0] : default(U);
                Q arg2 = args.Length > 1 ? (Q)args[1] : default(Q);
                E arg3 = args.Length > 2 ? (E)args[2] : default(E);
                action(arg1, arg2, arg3);
            };
            CheckAndAddListener<T>(listener, wrapper);
        }

        public void Subscribe<T, U, Q, E, Y>(object listener, Action<U, Q, E, Y> action) where T : class
        {
            Action<object[]> wrapper = args =>
            {
                U arg1 = args.Length > 0 ? (U)args[0] : default(U);
                Q arg2 = args.Length > 1 ? (Q)args[1] : default(Q);
                E arg3 = args.Length > 2 ? (E)args[2] : default(E);
                Y arg4 = args.Length > 3 ? (Y)args[3] : default(Y);
                action(arg1, arg2, arg3, arg4);
            };
            CheckAndAddListener<T>(listener, wrapper);
        }


        public void Unsubscribe<T>(object listener) where T : class
        {
            Type type = typeof(T);
            if (_subscriptions.TryGetValue(type, out var listenerMap))
            {
                listenerMap.Remove(listener);
                if (listenerMap.Count == 0)
                    _subscriptions.Remove(type);
            }
        }

        public void TriggerEvenet<T>() where T : class
        {
            TriggerEvenet<T>(Array.Empty<object>());
        }

        public void TriggerEvenet<T, U>(U arg) where T : class
        {
            TriggerEvenet<T>(arg);
        }

        public void TriggerEvenet<T, U, Q>(U arg, Q arg2) where T : class
        {
            TriggerEvenet<T>(arg, arg2);
        }

        public void TriggerEvenet<T, U, Q, E>(U arg, Q arg2, E arg3) where T : class
        {
            TriggerEvenet<T>(arg, arg2, arg3);
        }

        public void TriggerEvenet<T, U, Q, E, Y>(U arg, Q arg2, E arg3, Y arg4) where T : class
        {
            TriggerEvenet<T>(arg, arg2, arg3, arg4);
        }

        private void TriggerEvenet<T>(params object[] args) where T : class
        {
            Type type = typeof(T);
            if (!_subscriptions.TryGetValue(type, out var listenerMap))
                return;

            var listenersCopy = new List<KeyValuePair<object, List<Action<object[]>>>>(listenerMap);
            foreach (var kvp in listenersCopy)
            {
                foreach (var wrapper in kvp.Value)
                {
                    try
                    {
                        wrapper(args);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error invoking event {type} for listener {kvp.Key}: {e}");
                    }
                }
            }
        }
    }
}