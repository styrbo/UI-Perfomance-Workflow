﻿using UIWorkflow.Events;
using UnityEditor;
using UnityEngine;

namespace UIWorkflow.Events.Editor
{
    [CustomEditor(typeof(UIEventSender))]
    class UIEventSenderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = target as UIEventSender;

            var result = EditorGUILayout.Toggle("Visibility", t.Visible);
            
            if(result != t.Visible)
            {
                EditorUtility.SetDirty(t);
                Undo.RecordObject(t.Group, "UI Workflow Sender");
                t.Visible = result;
            }
        }
    }
}
