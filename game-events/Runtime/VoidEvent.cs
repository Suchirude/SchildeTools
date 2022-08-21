using UnityEngine;

namespace SchildeTools.GameEvents
{
    [CreateAssetMenu(menuName = "GameEvents/VoidEvent", fileName = "NewVoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}