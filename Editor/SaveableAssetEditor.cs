using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace DaBois.Saving.Editor
{
    namespace Dabois.Saving
    {
        [CustomEditor(typeof(SaveableAsset), true)]
        public class SaveableAssetEditor : UnityEditor.Editor
        {
            private SaveableAsset _x;

            private void OnEnable()
            {
                _x = (SaveableAsset)target;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                EditorGUILayout.BeginVertical("Box");

                EditorGUILayout.LabelField("Debug:");

                if (EditorApplication.isPlaying)
                {
                    EditorGUILayout.BeginHorizontal();
                    float originalWidth = UnityEditor.EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 50;
                    EditorGUILayout.PrefixLabel("Value:");
                    EditorGUIUtility.labelWidth = originalWidth;
                    _x.DrawDebug();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal("Box");

                    if (GUILayout.Button("Save"))
                    {
                        SaveManager.Instance.Save(true);
                    }
                    if (GUILayout.Button("Load"))
                    {
                        SaveManager.Instance.Load();
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
            }
        }
    }
}