using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    [CreateAssetMenu(fileName = "SaveableAssetLong", menuName = "DaBois/Save Systems/Asset Long")]
    public class SaveableAssetLong : SaveableAsset
    {
        [SerializeField]
        private long _defaultValue = default;
        private SaveableLong _saveable;

        public SaveableLong Saveable
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }
        public long Value
        {
            get
            {
                BaseInit();
                return _saveable.Value;
            }
        }

        protected override void Init()
        {
            _saveable = new SaveableLong(_path, _defaultValue);
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
            _saveable.Value = UnityEditor.EditorGUILayout.LongField(_saveable.Value);
        }
#endif
    }
}