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
            public class JsonableBase<T>
            {
                public string Key;
                public T Value;

                public JsonableBase(string key, T value)
                {
                    Key = key;
                    Value = value;
                }
            }

            [System.Serializable]
            public class JsonableString : JsonableBase<string>
            {
                //public string Value;
                public JsonableString(string key, string value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableInt : JsonableBase<int>
            {
                //public int Value;
                public JsonableInt(string key, int value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableBool : JsonableBase<bool>
            {
                //public bool Value;
                public JsonableBool(string key, bool value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableDouble : JsonableBase<double>
            {
                //public double Value;
                public JsonableDouble(string key, double value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableVector : JsonableBase<Vector3>
            {
                //public Vector3 Value;
                public JsonableVector(string key, Vector3 value) : base(key, value)
                {
                    //Value = value;
                }
            }

            [System.Serializable]
            public class JsonableStringArray : JsonableBase<string[]>
            {
                //public string[] Value;
                public JsonableStringArray(string key, string[] value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableIntArray : JsonableBase<int[]>
            {
                //public int[] Value;
                public JsonableIntArray(string key, int[] value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableBoolArray : JsonableBase<bool[]>
            {
                //public bool[] Value;
                public JsonableBoolArray(string key, bool[] value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableDoubleArray : JsonableBase<double[]>
            {
                //public double[] Value;
                public JsonableDoubleArray(string key, double[] value) : base(key, value)
                {
                    //Value = value;
                }
            }
            [System.Serializable]
            public class JsonableDictionary : JsonableBase<Dictionary<string, string>>
            {
                //public Dictionary<string, string> Value;
                public JsonableDictionary(string key, Dictionary<string, string> value) : base(key, value)
                {
                    //Value = value;
                }
            }
            

            [SerializeField]
            private List<JsonableString> _strings = new List<JsonableString>();
            private Dictionary<string, string> _stringsR = new Dictionary<string, string>();
            [SerializeField]
            private List<JsonableInt> _ints = new List<JsonableInt>();
            private Dictionary<string, int> _intsR = new Dictionary<string, int>();
            [SerializeField]
            private List<JsonableBool> _bools = new List<JsonableBool>();
            private Dictionary<string, bool> _boolsR = new Dictionary<string, bool>();
            [SerializeField]
            private List<JsonableDouble> _doubles = new List<JsonableDouble>();
            private Dictionary<string, double> _doublesR = new Dictionary<string, double>();
            [SerializeField]
            private List<JsonableVector> _vectors = new List<JsonableVector>();
            private Dictionary<string, Vector3> _vectorsR = new Dictionary<string, Vector3>();

            [SerializeField]
            private List<JsonableStringArray> _stringArrays = new List<JsonableStringArray>();
            private Dictionary<string, string[]> _stringArraysR = new Dictionary<string, string[]>();
            [SerializeField]
            private List<JsonableIntArray> _intArrays = new List<JsonableIntArray>();
            private Dictionary<string, int[]> _intArraysR = new Dictionary<string, int[]>();
            [SerializeField]
            private List<JsonableBoolArray> _boolArrays = new List<JsonableBoolArray>();
            private Dictionary<string, bool[]> _boolArraysR = new Dictionary<string, bool[]>();
            [SerializeField]
            private List<JsonableDoubleArray> _doubleArrays = new List<JsonableDoubleArray>();
            private Dictionary<string, double[]> _doubleArraysR = new Dictionary<string, double[]>();
            [SerializeField]
            private List<JsonableDictionary> _dictionaries = new List<JsonableDictionary>();
            private Dictionary<string, Dictionary<string, string>> _dictionariesR = new Dictionary<string, Dictionary<string, string>>();

            public JsonableData()
            {

            }

            public JsonableData(string json)
            {
                JsonableData newData = JsonUtility.FromJson<JsonableData>(json);

                _strings = newData._strings;
                _ints = newData._ints;
                _bools = newData._bools;
                _doubles = newData._doubles;
                _vectors = newData._vectors;
                _stringArrays = newData._stringArrays;
                _intArrays = newData._intArrays;
                _boolArrays = newData._boolArrays;
                _doubleArrays = newData._doubleArrays;
                _dictionaries = newData._dictionaries;

                UpdateDictionary(_strings, _stringsR);
                UpdateDictionary(_ints, _intsR);
                UpdateDictionary(_bools, _boolsR);
                UpdateDictionary(_doubles, _doublesR);
                UpdateDictionary(_vectors, _vectorsR);
                UpdateDictionary(_stringArrays, _stringArraysR);
                UpdateDictionary(_intArrays, _intArraysR);
                UpdateDictionary(_boolArrays, _boolArraysR);
                UpdateDictionary(_doubleArrays, _doubleArraysR);
                UpdateDictionary(_dictionaries, _dictionariesR);
            }

            private void UpdateDictionary<T, U>(List<T> jsonable, Dictionary<string, U> dictionary) where T : JsonableBase<U>
            {
                foreach (var i in jsonable)
                {
                    dictionary.Add(i.Key, i.Value);
                }
            }

            public void Add(string key, string value)
            {
                _strings.Add(new JsonableString(key, value));
                _stringsR.Add(key, value);
            }
            public void Add(string key, int value)
            {
                _ints.Add(new JsonableInt(key, value));
                _intsR.Add(key, value);
            }
            public void Add(string key, bool value)
            {
                _bools.Add(new JsonableBool(key, value));
                _boolsR.Add(key, value);
            }
            public void Add(string key, double value)
            {
                _doubles.Add(new JsonableDouble(key, value));
                _doublesR.Add(key, value);
            }
            public void Add(string key, Vector3 value)
            {
                _vectors.Add(new JsonableVector(key, value));
                _vectorsR.Add(key, value);
            }

            public void Add(string key, string[] value)
            {
                _stringArrays.Add(new JsonableStringArray(key, value));
                _stringArraysR.Add(key, value);
            }
            public void Add(string key, int[] value)
            {
                _intArrays.Add(new JsonableIntArray(key, value));
                _intArraysR.Add(key, value);
            }
            public void Add(string key, bool[] value)
            {
                _boolArrays.Add(new JsonableBoolArray(key, value));
                _boolArraysR.Add(key, value);
            }
            public void Add(string key, double[] value)
            {
                _doubleArrays.Add(new JsonableDoubleArray(key, value));
                _doubleArraysR.Add(key, value);
            }
            public void Add(string key, float[] value)
            {
                double[] casted = new double[value.Length];
                for (int i = 0; i < casted.Length; i++)
                {
                    casted[i] = (double)value[i];
                }

                _doubleArrays.Add(new JsonableDoubleArray(key, casted));
                _doubleArraysR.Add(key, casted);
            }
            public void Add(string key, long[] value)
            {
                double[] casted = new double[value.Length];
                for (int i = 0; i < casted.Length; i++)
                {
                    casted[i] = (double)value[i];
                }

                _doubleArrays.Add(new JsonableDoubleArray(key, casted));
                _doubleArraysR.Add(key, casted);
            }
            public void Add(string key, Dictionary<string, string> value)
            {
                _dictionaries.Add(new JsonableDictionary(key, value));
                _dictionariesR.Add(key, value);
            }

            public bool TryGet(string key, out string value)
            {
                if(_stringsR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = "";
                return false;
            }

            public bool TryGet(string key, out int value)
            {
                if (_intsR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out byte value)
            {
                if (_intsR.TryGetValue(key, out int val))
                {
                    value = (byte)val;
                    return true;
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out bool value)
            {
                if (_boolsR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = false;
                return false;
            }

            public bool TryGet(string key, out double value)
            {
                if (_doublesR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = 0;
                return false;
            }

            public bool TryGet(string key, out float value)
            {
                bool got = TryGet(key, out double val);
                value = (float)val;
                return got;
            }

            public bool TryGet(string key, out long value)
            {
                bool got = TryGet(key, out double val);
                value = (long)val;
                return got;
            }
            
            public bool TryGet(string key, out Vector3 value)
            {
                if (_vectorsR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = Vector3.zero;
                return false;
            }

            public bool TryGet(string key, out string[] value)
            {
                if (_stringArraysR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = new string[0];
                return false;
            }

            public bool TryGet(string key, out int[] value)
            {
                if (_intArraysR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = new int[0];
                return false;
            }            

            public bool TryGet(string key, out bool[] value)
            {
                if (_boolArraysR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = new bool[0];
                return false;
            }

            public bool TryGet(string key, out double[] value)
            {
                if (_doubleArraysR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = new double[0];
                return false;
            }

            public bool TryGet(string key, out float[] value)
            {
                bool got = TryGet(key, out double[] val);
                value = new float[val.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = (float)val[i];
                }
                return got;
            }

            public bool TryGet(string key, out long[] value)
            {
                bool got = TryGet(key, out double[] val);
                value = new long[val.Length];
                for(int i = 0; i < value.Length; i++)
                {
                    value[i] = (long)val[i];
                }
                return got;
            }

            public bool TryGet(string key, out Dictionary<string, string> value)
            {
                if (_dictionariesR.TryGetValue(key, out value))
                {
                    return true;
                }

                value = new Dictionary<string, string>();
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
                try{
                    Load();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }

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