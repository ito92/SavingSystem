using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DaBois.Saving
{
    public class SaveManager : MonoBehaviour
    {
        [System.Serializable]
        public class JsonableData
        {
            [System.Serializable]
            public class JsonableBase
            {
                public string Key;
                public JsonableBase(string key)
                {
                    Key = key;
                }
            }

            [System.Serializable]
            public class JsonableString : JsonableBase
            {
                public string Value;
                public JsonableString(string key, string value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableInt : JsonableBase
            {
                public int Value;
                public JsonableInt(string key, int value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableBool : JsonableBase
            {
                public bool Value;
                public JsonableBool(string key, bool value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableDouble : JsonableBase
            {
                public double Value;
                public JsonableDouble(string key, double value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableVector : JsonableBase
            {
                public Vector3 Value;
                public JsonableVector(string key, Vector3 value) : base(key)
                {
                    Value = value;
                }
            }

            [System.Serializable]
            public class JsonableStringArray : JsonableBase
            {
                public string[] Value;
                public JsonableStringArray(string key, string[] value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableIntArray : JsonableBase
            {
                public int[] Value;
                public JsonableIntArray(string key, int[] value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableBoolArray : JsonableBase
            {
                public bool[] Value;
                public JsonableBoolArray(string key, bool[] value) : base(key)
                {
                    Value = value;
                }
            }
            [System.Serializable]
            public class JsonableDoubleArray : JsonableBase
            {
                public double[] Value;
                public JsonableDoubleArray(string key, double[] value) : base(key)
                {
                    Value = value;
                }
            }

            [SerializeField]
            private List<JsonableString> _strings = new List<JsonableString>();
            [SerializeField]
            private List<JsonableInt> _ints = new List<JsonableInt>();
            [SerializeField]
            private List<JsonableBool> _bools = new List<JsonableBool>();
            [SerializeField]
            private List<JsonableDouble> _doubles = new List<JsonableDouble>();
            [SerializeField]
            private List<JsonableVector> _vectors = new List<JsonableVector>();

            [SerializeField]
            private List<JsonableStringArray> _stringArrays = new List<JsonableStringArray>();
            [SerializeField]
            private List<JsonableIntArray> _intArrays = new List<JsonableIntArray>();
            [SerializeField]
            private List<JsonableBoolArray> _boolArrays = new List<JsonableBoolArray>();
            [SerializeField]
            private List<JsonableDoubleArray> _doubleArrays = new List<JsonableDoubleArray>();

            public JsonableData()
            {

            }

            public JsonableData(string json)
            {
                JsonUtility.FromJson<JsonableData>(json);
            }

            public void Add(string key, string value)
            {
                _strings.Add(new JsonableString(key, value));
            }
            public void Add(string key, int value)
            {
                _ints.Add(new JsonableInt(key, value));
            }
            public void Add(string key, bool value)
            {
                _bools.Add(new JsonableBool(key, value));
            }
            public void Add(string key, double value)
            {
                _doubles.Add(new JsonableDouble(key, value));
            }
            public void Add(string key, Vector3 value)
            {
                _vectors.Add(new JsonableVector(key, value));
            }

            public void Add(string key, string[] value)
            {
                _stringArrays.Add(new JsonableStringArray(key, value));
            }
            public void Add(string key, int[] value)
            {
                _intArrays.Add(new JsonableIntArray(key, value));
            }
            public void Add(string key, bool[] value)
            {
                _boolArrays.Add(new JsonableBoolArray(key, value));
            }
            public void Add(string key, double[] value)
            {
                _doubleArrays.Add(new JsonableDoubleArray(key, value));
            }
            public void Add(string key, float[] value)
            {
                double[] casted = new double[value.Length];
                for (int i = 0; i < casted.Length; i++)
                {
                    casted[i] = (double)value[i];
                }

                _doubleArrays.Add(new JsonableDoubleArray(key, casted));
            }
            public void Add(string key, long[] value)
            {
                double[] casted = new double[value.Length];
                for (int i = 0; i < casted.Length; i++)
                {
                    casted[i] = (double)value[i];
                }

                _doubleArrays.Add(new JsonableDoubleArray(key, casted));
            }

            public bool TryGet(string key, out string value)
            {
                for (int i = 0; i < _strings.Count; i++)
                {
                    if (_strings[i].Key == key)
                    {
                        value = _strings[i].Value;
                        return true;
                    }
                }

                value = "";
                return false;
            }

            public bool TryGet(string key, out int value)
            {
                for (int i = 0; i < _ints.Count; i++)
                {
                    if (_ints[i].Key == key)
                    {
                        value = _ints[i].Value;
                        return true;
                    }
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out bool value)
            {
                for (int i = 0; i < _bools.Count; i++)
                {
                    if (_bools[i].Key == key)
                    {
                        value = _bools[i].Value;
                        return true;
                    }
                }

                value = false;
                return false;
            }

            public bool TryGet(string key, out double value)
            {
                for (int i = 0; i < _doubles.Count; i++)
                {
                    if (_doubles[i].Key == key)
                    {
                        value = _doubles[i].Value;
                        return true;
                    }
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out float value)
            {
                for (int i = 0; i < _doubles.Count; i++)
                {
                    if (_doubles[i].Key == key)
                    {
                        value = (float)_doubles[i].Value;
                        return true;
                    }
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out long value)
            {
                for (int i = 0; i < _doubles.Count; i++)
                {
                    if (_doubles[i].Key == key)
                    {
                        value = (long)_doubles[i].Value;
                        return true;
                    }
                }

                value = 0;
                return false;
            }
            
            public bool TryGet(string key, out Vector3 value)
            {
                for (int i = 0; i < _vectors.Count; i++)
                {
                    if (_vectors[i].Key == key)
                    {
                        value = (Vector3)_vectors[i].Value;
                        return true;
                    }
                }

                value = Vector3.zero;
                return false;
            }

            public bool TryGet(string key, out string[] value)
            {
                for (int i = 0; i < _stringArrays.Count; i++)
                {
                    if (_stringArrays[i].Key == key)
                    {
                        value = _stringArrays[i].Value;
                        return true;
                    }
                }

                value = new string[0];
                return false;
            }

            public bool TryGet(string key, out int[] value)
            {
                for (int i = 0; i < _intArrays.Count; i++)
                {
                    if (_intArrays[i].Key == key)
                    {
                        value = _intArrays[i].Value;
                        return true;
                    }
                }

                value = new int[0];
                return false;
            }

            public bool TryGet(string key, out bool[] value)
            {
                for (int i = 0; i < _boolArrays.Count; i++)
                {
                    if (_boolArrays[i].Key == key)
                    {
                        value = _boolArrays[i].Value;
                        return true;
                    }
                }

                value = new bool[0];
                return false;
            }

            public bool TryGet(string key, out double[] value)
            {
                for (int i = 0; i < _doubleArrays.Count; i++)
                {
                    if (_doubleArrays[i].Key == key)
                    {
                        value = _doubleArrays[i].Value;
                        return true;
                    }
                }

                value = new double[0];
                return false;
            }

            public bool TryGet(string key, out float[] value)
            {
                for (int i = 0; i < _doubleArrays.Count; i++)
                {
                    if (_doubleArrays[i].Key == key)
                    {
                        float[] casted = new float[_doubleArrays[i].Value.Length];
                        for (int j = 0; j < _doubleArrays[i].Value.Length; j++)
                        {
                            casted[j] = (float)_doubleArrays[i].Value[j];
                        }

                        value = casted;
                        return true;
                    }
                }

                value = new float[0];
                return false;
            }

            public bool TryGet(string key, out long[] value)
            {
                for (int i = 0; i < _doubleArrays.Count; i++)
                {
                    if (_doubleArrays[i].Key == key)
                    {
                        long[] casted = new long[_doubleArrays[i].Value.Length];
                        for (int j = 0; j < _doubleArrays[j].Value.Length; j++)
                        {
                            casted[j] = (long)_doubleArrays[i].Value[j];
                        }

                        value = casted;
                        return true;
                    }
                }

                value = new long[0];
                return false;
            }

            public string Json()
            {
                return JsonUtility.ToJson(this);
            }
        }

        [System.Serializable]
        public class Settings
        {
            private SaveableAsset[] _saveables;
            public SaveableAsset[] Saveables
            {
                get
                {
                    return _saveables;
                }
            }

            public SaveableAsset[] CustomSaveables
            {
                get
                {
                    return _customSaveables;
                }
            }

            [SerializeField]
            private SaveableAsset[] _customSaveables = default;

            public void Setup()
            {
                List<SaveableAsset> saveables = new List<SaveableAsset>();
                _saveables = saveables.ToArray();
            }

            public string GetJson()
            {
                JsonableData jsonable = new JsonableData();

                for (int i = 0; i < _saveables.Length; i++)
                {
                    _saveables[i].AddToJsonable(ref jsonable);
                }

                for (int i = 0; i < _customSaveables.Length; i++)
                {
                    _customSaveables[i].AddToJsonable(ref jsonable);
                }

                return jsonable.Json();
            }

            public void SetFromJson(string json)
            {
                JsonableData jsonable = JsonUtility.FromJson<JsonableData>(json);

                for (int i = 0; i < _saveables.Length; i++)
                {
                    _saveables[i].FromJsonable(jsonable);
                }

                for (int i = 0; i < _customSaveables.Length; i++)
                {
                    _customSaveables[i].FromJsonable(jsonable);
                }
                SaveManager.Instance.Save(true);
            }
        }

        private static SaveManager _instance;
        public static SaveManager Instance
        {
            get
            {
                return _instance;
            }
        }

        [SerializeField]
        private string _savePath = "company.dat";
        [SerializeField]
        private SaveableAsset[] _customSaveables = default;
        [SerializeField]
        private SaveableLong _updateDate = new SaveableLong("UD", 0);
        private long _loadDate;
        [SerializeField]
        private bool _autoLoad = true;

        private SaverInterface _saverInterface;

        public delegate void d_onSave();
        public d_onSave OnSave;
        public delegate void d_onSaveAttempt();
        public d_onSaveAttempt OnSaveAttempt;

        private float _saveRate = 1;
        private bool _savePending;
        private float _currentSaveRate;

        private string AbsoluteSavePath
        {
            get
            {
                return Application.persistentDataPath + "/" + _savePath;
            }
        }

        public SaveableAsset[] CustomSaveables
        {
            get
            {
                return _customSaveables;
            }
        }

        public SaveableLong UpdateDate
        {
            get
            {
                return _updateDate;
            }
        }

        public long LoadDate
        {
            get
            {
                return _loadDate;
            }
        }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            _saverInterface = new SaverInterface(AbsoluteSavePath);

            if (_autoLoad)
            {
                Load();
            }

            _loadDate = _updateDate.Value;
            SaveInteral(true, false);
        }

        public bool DataExist<T>(SaveablePropertyBase<T> savable)
        {
            return savable.Exists(_saverInterface);
        }

        /// <summary>
        /// Adjust the array size to a original database, returns the new array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveable">Saveable Property to modify</param>
        /// <param name="saved">Array saved on disk or memory</param>
        /// <param name="original">Original array with correct size</param>
        /// <returns></returns>
        public T[] NormalizeSaveableArray<T>(SaveablePropertyBase<T[]> saveable, T[] saved, T[] original)
        {
            if (DataExist(saveable))
            {
                if (saved.Length != original.Length)
                {
                    T[] temp = new T[original.Length];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = original[i];
                        if (i < saved.Length)
                        {
                            temp[i] = saved[i];
                        }
                    }

                    saved = temp;
                }

                return saved;
            }
            else
            {
                return original;
            }
        }

        public void Load()
        {
            if (!SavingInternal.FileExists(AbsoluteSavePath))
            {
                Debug.Log("Creating Save file for the first time");
                _saverInterface.Save();
            }

            Debug.Log("Loading data");
            _saverInterface.Load();
            Load(_saverInterface);
        }

        /// <summary>
        /// Load Custom Saveables
        /// </summary>
        /// <param name="saveables"></param>
        public void Load<T>(SaveablePropertyBase<T>[] saveables)
        {
            CustomLoad(saveables, _saverInterface);
        }

        private void CustomLoad<T>(SaveablePropertyBase<T>[] saveables, SaverInterface saverInterface)
        {
            for (int i = 0; i < saveables.Length; i++)
            {
                saveables[i].Load(ref saverInterface);
            }
        }

        public void Load(SaverInterface saverInterface)
        {
            _updateDate.Load(ref saverInterface);
            for (int i = 0; i < _customSaveables.Length; i++)
            {
                _customSaveables[i].Load(ref saverInterface);
            }
        }

        public void Save(bool force = false)
        {
            SaveInteral(force, true);
        }

        private void SaveInteral(bool force, bool date = true)
        {
            if (OnSaveAttempt != null)
            {
                OnSaveAttempt();
            }

            if (force)
            {
                _currentSaveRate = _saveRate;
            }
            if (_currentSaveRate > 0)
            {
                _savePending = true;
                return;
            }

            _currentSaveRate = _saveRate;

            if (date)
            {
                Save(_saverInterface);
            }
            else
            {
                SaveNoDate(_saverInterface);
            }
            if (OnSave != null)
            {
                OnSave();
            }
        }

        /// <summary>
        /// Save custom saveables
        /// </summary>
        /// <param name="saveables"></param>
        public void Save<T>(SaveablePropertyBase<T>[] saveables)
        {
            CustomSave(saveables, _saverInterface);
        }

        private void CustomSave<T>(SaveablePropertyBase<T>[] saveables, SaverInterface _saverInterface)
        {
            for (int i = 0; i < saveables.Length; i++)
            {
                saveables[i].Save(ref _saverInterface);
            }

            _saverInterface.Save();
        }

        public void Save(SaverInterface saverInterface)
        {
            _updateDate.Value = System.DateTime.Now.Ticks;
            _updateDate.Save(ref saverInterface);
            for (int i = 0; i < _customSaveables.Length; i++)
            {
                _customSaveables[i].Save(ref saverInterface);
            }

            saverInterface.Save();
        }

        private void SaveNoDate(SaverInterface saverInterface)
        {
            _updateDate.Save(ref saverInterface);
            for (int i = 0; i < _customSaveables.Length; i++)
            {
                _customSaveables[i].Save(ref saverInterface);
            }

            saverInterface.Save();
        }

        public void DeleteSave()
        {
            SavingInternal.DeleteFile(AbsoluteSavePath);
            _updateDate.Value = 0;
            for (int i = 0; i < _customSaveables.Length; i++)
            {
                _customSaveables[i].ResetValues();
            }
        }

        private void Update()
        {
            if (_currentSaveRate > 0)
            {
                _currentSaveRate -= Time.unscaledDeltaTime;
            }
            else if (_savePending)
            {
                _savePending = false;
                Save();
            }
        }

        private void OnApplicationQuit()
        {
            Save(true);
        }

#if UNITY_EDITOR
        [ContextMenu("SAVE")]
        private void EditorSave()
        {
            Save(true);
        }
#endif
    }
}