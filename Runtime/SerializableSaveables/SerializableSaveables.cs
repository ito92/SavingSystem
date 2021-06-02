using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DaBois.Saving
{
    public class SerializableSaveables
    {
        public static void Serialize<T>(MemoryStream stream, BinaryFormatter bf, T value)
        {
            Debug.Log("Serializing generic: " + value);
            bf.Serialize(stream, value);
        }

        public static void Deserialize<T>(MemoryStream stream, BinaryFormatter bf, out T value)
        {
            value = (T)bf.Deserialize(stream);
        }

        public static void Serialize(MemoryStream stream, BinaryFormatter bf, int value)
        {
            Debug.Log("Serializing int: " + value);
            Serialize<int>(stream, bf, value);
        }

        public static void Deserialize(MemoryStream stream, BinaryFormatter bf, out int value)
        {
            value = (int)bf.Deserialize(stream);
        }

        public static void Serialize(MemoryStream stream, BinaryFormatter bf, float value)
        {
            Serialize<float>(stream, bf, value);
        }

        public static void Deserialize(MemoryStream stream, BinaryFormatter bf, out float value)
        {
            value = (float)bf.Deserialize(stream);
        }

        [Serializable]
        public struct SerializeableVector2
        {
            public float x;
            public float y;

            public SerializeableVector2(Vector2 vector)
            {
                x = vector.x;
                y = vector.y;
            }

            public Vector2 Deserialize()
            {
                return new Vector2(x, y);
            }
        }

        public static void Serialize(MemoryStream stream, BinaryFormatter bf, Vector2 value)
        {
            Debug.Log("Serializing vector2: " + value);
            Serialize<SerializeableVector2>(stream, bf, new SerializeableVector2(value));
        }

        public static void Deserialize(MemoryStream stream, BinaryFormatter bf, out Vector2 value)
        {
            value = ((SerializeableVector2)bf.Deserialize(stream)).Deserialize();
        }
    }
}