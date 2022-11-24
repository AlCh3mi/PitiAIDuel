// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using RoboRyanTron.Events;
using UnityEditor;
using UnityEngine;

namespace Events.Editor
{
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class EventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}
