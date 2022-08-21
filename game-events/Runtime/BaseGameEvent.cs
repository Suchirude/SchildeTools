using System.Collections.Generic;
using UnityEngine;

namespace SchildeTools.GameEvents
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly HashSet<IGameEventListener<T>> _listeners = new HashSet<IGameEventListener<T>>();

        protected void Raise(T item)
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> gameEventListener) => _listeners.Add(gameEventListener);
        public void UnregisterListener(IGameEventListener<T> gameEventListener) => _listeners.Remove(gameEventListener);

    }
}