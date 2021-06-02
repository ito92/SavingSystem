using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DaBois.Saving
{
    public class SaverInterface
    {
        private string _path;
        private Dictionary<string, byte[]> _savedKeys = new Dictionary<string, byte[]>();

        public SaverInterface(string path)
        {
            _path = path;
            _savedKeys = new Dictionary<string, byte[]>();
        }

        public void Load()
        {
            _savedKeys = SavingInternal.DeserializeSaveFile(_path);
        }

        public bool KeyExists(string key)
        {
            return _savedKeys.ContainsKey(key);
        }

        public void Write<T>(string key, SerializedSaveable<T> value)
        {
            if (_savedKeys.TryGetValue(key, out byte[] val))
            {
                _savedKeys[key] = value.Serialize();
            }
            else
            {
                _savedKeys.Add(key, value.Serialize());
            }
        }

        public T Read<T>(string key, T defaultValue, SerializedSaveable<T> serializer)
        {
            if(_savedKeys.TryGetValue(key, out byte[] bytes))
            {
                return serializer.Deserialize(bytes);
            }
            return defaultValue;
        }

        public void Save()
        {
            SavingInternal.SerializeSaveFile(_path, _savedKeys);
        }

        public void DeleteFile()
        {
            SavingInternal.DeleteFile(_path);
            _savedKeys.Clear();
        }
    }
}