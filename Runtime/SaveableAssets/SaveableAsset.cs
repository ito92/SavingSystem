using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaBois.Saving
{
    public abstract class SaveableAsset : ScriptableObject
    {
        [SerializeField]
        protected string _path;
        [NonSerialized]
        private bool _init;

        /*public virtual SaveableProperties<SaveableAsset> SaveableBase
        {
            get
            {
                return null;
            }
        }*/

        protected void BaseInit()
        {
            if (!_init)
            {
                _init = true;
                Init();
            }
        }
        protected virtual void Init() { }
        public virtual void ResetValues() { }

        public void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            BaseInit();
            AddToJsonableInternal(ref jsonable);
        }

        public void FromJsonable(SaveManager.JsonableData jsonable)
        {
            BaseInit();
            FromJsonableInternal(jsonable);
        }

        public void Load(ref SaverInterface saver)
        {
            BaseInit();
            LoadInternal(ref saver);
        }

        public void Save(ref SaverInterface saver)
        {
            BaseInit();
            SaveInternal(ref saver);
        }

        protected abstract void AddToJsonableInternal(ref SaveManager.JsonableData jsonable);

        protected abstract void FromJsonableInternal(SaveManager.JsonableData jsonable);

        protected abstract void LoadInternal(ref SaverInterface saver);

        protected abstract void SaveInternal(ref SaverInterface saver);

        #region Editor

        public abstract void DrawDebug();

        #endregion
    }
}