// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace RoboRyanTron.Events
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _eventListeners = new();

        public virtual void Raise()
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        public virtual void RegisterListener(GameEventListener listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public virtual void UnregisterListener(GameEventListener listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}