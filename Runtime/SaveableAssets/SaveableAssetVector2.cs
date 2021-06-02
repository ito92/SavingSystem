using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    [CreateAssetMenu(fileName = "SaveableAssetVector2", menuName = "DaBois/Save Systems/Asset Vector2")]
    public class SaveableAssetVector2 : SaveableAsset
    {
        [SerializeField]
        private Vector2 _defaultValue = default;
        private SaveableVector2 _saveable;

        public SaveableVector2 Saveable
        {
            get
            {
                BaseInit();
                return _saveable;
            }
        }
        public Vector2 Value
        {
            get
            {
                BaseInit();
                return _saveable.Value;
            }
        }

        protected override void Init()
        {
            _saveable = new SaveableVector2(_path, _defaultValue);
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
            _saveable.Value = UnityEditor.EditorGUILayout.Vector2Field("", _saveable.Value);
        }
#endif
    }
}