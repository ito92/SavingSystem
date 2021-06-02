using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DaBois.Saving.Editor
{
    namespace Dabois.Saving
    {
        [CustomPropertyDrawer(typeof(SaveableProperties<>), true)]
        public class SaveablePropertyBaseDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                GUI.Box(position, "");
                DrawPropery(property, "_path", ref position);
                DrawPropery(property, "Value", ref position);
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                SerializedProperty path = property.FindPropertyRelative("_path");
                SerializedProperty value = property.FindPropertyRelative("Value");
                return EditorGUI.GetPropertyHeight(path, true) + EditorGUI.GetPropertyHeight(value, true);
            }

            private void DrawPropery(SerializedProperty property, string propertyName, ref Rect position)
            {
                SerializedProperty value = property.FindPropertyRelative(propertyName);
                position.height = EditorGUI.GetPropertyHeight(value, true);
                EditorGUI.PropertyField(position, value);
                position.y += position.height;
            }
        }
    }
}