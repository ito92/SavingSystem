using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    [CreateAssetMenu(fileName = "SaveableAssetVector3", menuName = "DaBois/Save Systems/Asset Vector3")]
    public class SaveableAssetVector3 : SaveableAsset
    {
        [SerializeField]
        private Vector3 _defaultValue = default;
        private SaveableVector3 _saveable;

        public SaveableVector3 Saveable
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }
        public Vector3 Value
        {
            get
            {
                BaseInit();
                return _saveable.Value;
            }
        }

        protected override void Init()
        {
            _saveable = new SaveableVector3(_path, _defaultValue);
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
            _saveable.Value = UnityEditor.EditorGUILayout.Vector3Field("", _saveable.Value);
        }
#endif
    }
}