using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    [CreateAssetMenu(fileName = "SaveableAssetArrayFloat", menuName = "DaBois/Save Systems/Asset Array Float")]
    public class SaveableAssetFloatArray : SaveableAsset
    {
        [SerializeField]
        private float[] _defaultValue = default;
        private SaveableFloatArray _saveable;

        public SaveableFloatArray Saveable
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }
        public float[] Value
        {
            get
            {
                BaseInit();
                return _saveable.Value;
            }
        }

        protected override void Init()
        {
            _saveable = new SaveableFloatArray(_path, _defaultValue);
        }
        public override void ResetValues()
        {
            BaseInit();
            _saveable.Value = _defaultValue;
        }
        protected override void AddToJsonableInternal(ref SaveManager.JsonableData jsonable)
        {
            _saveable.AddToJsonable(ref jsonable);
        }
        protected override void FromJsonableInternal(SaveManager.JsonableData jsonable)
        {
            _saveable.FromJsonable(jsonable);
        }
        protected override void LoadInternal(ref SaverInterface saver)
        {
            _saveable.Load(ref saver);
        }
        protected override void SaveInternal(ref SaverInterface saver)
        {
            _saveable.Save(ref saver);
        }
#if UNITY_EDITOR
        public override void DrawDebug()
        {
            float originalWidth = UnityEditor.EditorGUIUtility.labelWidth;
            UnityEditor.EditorGUIUtility.labelWidth = 20;
            UnityEditor.EditorGUILayout.BeginVertical();
            for (int i = 0; i < _saveable.Value.Length; i++)
                _saveable.Value[i] = UnityEditor.EditorGUILayout.FloatField(i.ToString(), _saveable.Value[i]);
            UnityEditor.EditorGUILayout.EndVertical();
            UnityEditor.EditorGUIUtility.labelWidth = originalWidth;
        }
#endif
    }
}