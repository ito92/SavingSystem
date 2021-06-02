using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DaBois.Saving
{
    public class SavingInternal
    {
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        private static FileStream GetOrCreateFile(string filePath)
        {
            return File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static Dictionary<string, byte[]> DeserializeSaveFile(string filePath)
        {
            Dictionary<string, byte[]> savedKeys = new Dictionary<string, byte[]>();
            byte[] bytesData = File.ReadAllBytes(filePath);

            Stream stream = new MemoryStream(bytesData);

            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string rKey = reader.ReadString();
                    int size = reader.ReadInt32();
                    byte[] rValue = reader.ReadBytes(size);
                    savedKeys.Add(rKey, rValue);
                }
            }

            return savedKeys;
        }

        public static void SerializeSaveFile(string filePath, Dictionary<string, byte[]> savedKeys)
        {
            using (FileStream file = GetOrCreateFile(filePath))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    foreach (KeyValuePair<string, byte[]> pairs in savedKeys)
                    {
                        writer.Write(pairs.Key);
                        writer.Write(pairs.Value.Length);
                        writer.Write(pairs.Value);
                    }
                }
            }
        }

        public static bool KeyExists(string filePath, string key)
        {
            Dictionary<string, byte[]> savedKeys = DeserializeSaveFile(filePath);
            return savedKeys.ContainsKey(key);
        }

        public static void Write<T>(string filePath, string key, T value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Dictionary<string, byte[]> savedKeys = DeserializeSaveFile(filePath);
            if(savedKeys.TryGetValue(key, out byte[] val))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, value);
                    savedKeys[key] = ms.ToArray();
                }
            }

            SerializeSaveFile(filePath, savedKeys);
        }
    }
}