using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    [CreateAssetMenu(fileName = "SaveableAssetInt", menuName = "DaBois/Save Systems/Asset Int")]
    public class SaveableAssetInt : SaveableAsset
    {
        [SerializeField]
        private int _defaultValue = default;
        private SaveableInt _saveable;

        /*public override SaveableProperties<SaveableAsset> SaveableBase
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }*/

        public SaveableInt Saveable
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }
        public int Value
        {
            get
            {
                BaseInit();
                return _saveable.Value;
            }
        }

        protected override void Init()
        {
            _saveable = new SaveableInt(_path, _defaultValue);
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
            _saveable.Value = UnityEditor.EditorGUILayout.IntField(_saveable.Value);
        }
#endif
    }
}