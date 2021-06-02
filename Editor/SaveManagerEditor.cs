using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DaBois.Saving.Editor
{
    namespace Dabois.Saving
    {
        [CustomEditor(typeof(SaveManager))]
        public class SaveManagerEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (EditorApplication.isPlaying)
                {
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

                if(GUILayout.Button("Open file path"))
                {
                    //EditorUtility.RevealInFinder(Application.persistentDataPath + "/" + serializedObject.FindProperty("_savePath").stringValue);
                    EditorUtility.OpenWithDefaultApp(Application.persistentDataPath + "/");
                }
                if (GUILayout.Button("Delete save file"))
                {
                    if (EditorUtility.DisplayDialog("Delete Save file", "Are you sure?", "Yes", "No"))
                    {
                        SavingInternal.DeleteFile(Application.persistentDataPath + "/" + serializedObject.FindProperty("_savePath").stringValue);
                    }
                }
            }
        }
    }
}