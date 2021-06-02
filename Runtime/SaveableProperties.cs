using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DaBois.Saving
{
    [System.Serializable]
    public abstract class SaveablePropertyBase<T>
    {
        [SerializeField]
        protected string _path = default;

        public string Path
        {
            get
            {
                return _path;
            }
        }

        public void SetPath(string path)
        {
            _path = path;
        }

        public bool Exists(SaverInterface saverInterface)
        {
            return saverInterface.KeyExists(_path);
        }

        /// <summary>
        /// Save Serialized
        /// </summary>
        /// <param name="saverInterface"></param>
        public void Save(ref SaverInterface saverInterface)
        {
            InternalSave(ref saverInterface);
        }

        /// <summary>
        /// Save to PlayerPrefs
        /// </summary>
        public void Save()
        {
            InternalSavePref();
        }

        /// <summary>
        /// Load serialized
        /// </summary>
        /// <param name="saverInterface"></param>
        public void Load(ref SaverInterface saverInterface)
        {
            InternalLoad(ref saverInterface);
        }

        /// <summary>
        /// Load from PlayerPrefs
        /// </summary>
        public void Load()
        {
            if (PlayerPrefs.HasKey(_path))
            {
                InternalLoadPref();
            }
            else
            {
                LoadDefaultValue();
            }
        }

        protected virtual void InternalSave(ref SaverInterface saverInterface)
        {

        }

        protected void DefaultInternalSave(ref SaverInterface saverInterface, SerializedSaveable<T> value)
        {
            saverInterface.Write(_path, value);
        }

        protected virtual void InternalSavePref()
        {

        }

        protected virtual void InternalLoad(ref SaverInterface saverInterface)
        {

        }

        protected void DefaultInternalLoad<U>(ref SaverInterface saverInterface, ref U defaultValue, SerializedSaveable<U> serializer)
        {
            defaultValue = saverInterface.Read(_path, defaultValue, serializer);
        }

        protected virtual void InternalLoadPref()
        {

        }

        protected virtual void LoadDefaultValue()
        {

        }

        public virtual void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {

        }

        public virtual void FromJsonable(SaveManager.JsonableData jsonable)
        {

        }
    }

    [System.Serializable]
    public class SaveableProperties<T> : SaveablePropertyBase<T>, SerializedSaveable<T>
    {
        public SaveableProperties(string path)
        {
            _path = path;
        }

        public virtual byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public virtual T Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }
    }

    [System.Serializable]
    public class SaveableBool : SaveableProperties<bool>
    {
        public bool Value;

        public SaveableBool(string path, bool defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override bool Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return reader.ReadBoolean();
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetInt(_path, Value ? 1 : 0);
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = PlayerPrefs.GetInt(_path) == 0 ? false : true;
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableBoolArray : SaveableProperties<bool[]>
    {
        public bool[] Value;        

        public SaveableBoolArray(string path, bool[] defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.Length);
                    for (int i = 0; i < Value.Length; i++)
                    {
                        writer.Write(Value[i]);
                    }
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override bool[] Deserialize(byte[] data)
        {
            bool[] val = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    val = new bool[reader.ReadInt32()];
                    for (int i = 0; i < val.Length; i++)
                    {
                        val[i] = reader.ReadBoolean();
                    }
                }
            }

            return val;
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableInt : SaveableProperties<int>
    {
        public int Value;

        public SaveableInt(string path, int defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override int Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return reader.ReadInt32();
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetInt(_path, Value);
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {            
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = PlayerPrefs.GetInt(_path);
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableIntArray : SaveableProperties<int[]>
    {
        public int[] Value;

        public SaveableIntArray(string path, int[] defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.Length);
                    for (int i = 0; i < Value.Length; i++)
                    {
                        writer.Write(Value[i]);
                    }
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override int[] Deserialize(byte[] data)
        {
            int[] val = null;
            using (MemoryStream stream = new MemoryStream(data))
            {                
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    val = new int[reader.ReadInt32()];
                    for (int i = 0; i < val.Length; i++)
                    {
                        val[i] = reader.ReadInt32();
                    }
                }
            }

            return val;
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableFloat : SaveableProperties<float>
    {
        public float Value;

        public SaveableFloat(string path, float defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override float Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return reader.ReadSingle();
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetFloat(_path, Value);
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = PlayerPrefs.GetFloat(_path);
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableFloatArray : SaveableProperties<float[]>
    {
        public float[] Value;

        public SaveableFloatArray(string path, float[] defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.Length);
                    for (int i = 0; i < Value.Length; i++)
                    {
                        writer.Write(Value[i]);
                    }
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override float[] Deserialize(byte[] data)
        {
            float[] val = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    val = new float[reader.ReadInt32()];
                    for (int i = 0; i < val.Length; i++)
                    {
                        val[i] = reader.ReadSingle();
                    }
                }
            }

            return val;
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableLong : SaveableProperties<long>
    {
        public long Value;

        public SaveableLong(string path, long defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override long Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return reader.ReadInt64();
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetString(_path, Value.ToString());
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = long.Parse(PlayerPrefs.GetString(_path));
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableLongArray : SaveableProperties<long[]>
    {
        public long[] Value;

        public SaveableLongArray(string path, long[] defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.Length);
                    for (int i = 0; i < Value.Length; i++)
                    {
                        writer.Write(Value[i]);
                    }
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override long[] Deserialize(byte[] data)
        {
            long[] val = null;
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    val = new long[reader.ReadInt32()];
                    for (int i = 0; i < val.Length; i++)
                    {
                        val[i] = reader.ReadInt64();
                    }
                }
            }

            return val;
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            throw new NotSupportedException("Arrays cannot be saved to PlayerPrefs");
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }

    [System.Serializable]
    public class SaveableString : SaveableProperties<string>
    {
        public string Value;

        public SaveableString(string path, string defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override string Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return reader.ReadString();
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetString(_path, Value.ToString());
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = PlayerPrefs.GetString(_path);
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Value);
        }
    }
    
    [System.Serializable]
    public class SaveableVector2 : SaveableProperties<Vector2>
    {
        public Vector2 Value;

        public SaveableVector2(string path, Vector2 defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.x);
                    writer.Write(Value.y);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override Vector2 Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return new Vector2(reader.ReadSingle(), reader.ReadSingle());
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetString(_path, Value.ToString());
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = new Vector2(PlayerPrefs.GetFloat(_path + "X"), PlayerPrefs.GetFloat(_path + "Y"));
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Vector3 vec);
            Value = vec;
        }
    }

    [System.Serializable]
    public class SaveableVector3 : SaveableProperties<Vector3>
    {
        public Vector3 Value;

        public SaveableVector3(string path, Vector3 defaultValue) : base(path)
        {
            Value = defaultValue;
        }

        public override byte[] Serialize()
        {
            byte[] bytes = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Value.x);
                    writer.Write(Value.y);
                    writer.Write(Value.z);
                }
                bytes = stream.GetBuffer();
            }
            return bytes;
        }

        public override Vector3 Deserialize(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }
            }
        }

        protected override void InternalSave(ref SaverInterface saverInterface)
        {
            DefaultInternalSave(ref saverInterface, this);
        }

        protected override void InternalSavePref()
        {
            PlayerPrefs.SetString(_path, Value.ToString());
        }

        protected override void InternalLoad(ref SaverInterface saverInterface)
        {
            DefaultInternalLoad(ref saverInterface, ref Value, this);
        }

        protected override void InternalLoadPref()
        {
            Value = new Vector3(PlayerPrefs.GetFloat(_path + "X"), PlayerPrefs.GetFloat(_path + "Y"), PlayerPrefs.GetFloat(_path + "Z"));
        }

        public override void AddToJsonable(ref SaveManager.JsonableData jsonable)
        {
            jsonable.Add(Path, Value);
        }

        public override void FromJsonable(SaveManager.JsonableData jsonable)
        {
            jsonable.TryGet(Path, out Vector3 vec);
            Value = vec;
        }
    }
}