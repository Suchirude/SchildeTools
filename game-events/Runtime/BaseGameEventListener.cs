using UnityEngine;
using UnityEngine.Events;

namespace SchildeTools.GameEvents
{
    public abstract class BaseGameEventListener<T, TE, TUer> : MonoBehaviour, IGameEventListener<T> where TE : BaseGameEvent<T> where TUer : UnityEvent<T>
    {
        [SerializeField] protected TE gameEvent;
        private TE GameEvent => gameEvent;
    

        [SerializeField] protected TUer unityEventResponse;

        public virtual void OnEventRaised(T item) => unityEventResponse?.Invoke(item);
        
        
        private void OnEnable()
        {
            if (gameEvent == null) return;
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;
            GameEvent.UnregisterListener(this);
        }
    }
}