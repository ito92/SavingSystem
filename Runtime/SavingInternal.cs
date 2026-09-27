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

        /// <summary>What a save being written is called until it is finished.</summary>
        private const string Writing = ".tmp";
        /// <summary>What the save before this one is kept as.</summary>
        public const string Backup = ".bak";

        private static FileStream GetOrCreateFile(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create, not OpenOrCreate: OpenOrCreate writes over the start of
            // the old file and leaves whatever was longer than the new save
            // sitting past the end of it. The reader then parses that tail as
            // another section, which is what "corrupted save" nearly always
            // turns out to be.
            return File.Open(filePath, FileMode.Create, FileAccess.Write);
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);

            // The backup and any half-written file go with it: a save deleted
            // and then loaded should be gone, not quietly restored from
            // beside itself.
            Forget(filePath + Backup);
            Forget(filePath + Writing);
        }

        private static void Forget(string filePath)
        {
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogWarning("[Saving] " + filePath + " could not be deleted: " + e.Message);
            }
        }

        /// <summary>
        /// Reads a save file. Never throws: a file that cannot be read gives
        /// back whatever of it could be, and says so.
        ///
        /// A save is a run of sections, each a key, a length and that many
        /// bytes. One section that does not read leaves the ones before it
        /// perfectly good, so they are kept rather than thrown away with it —
        /// a broken list of buildables should not cost a player their pets.
        /// The file is then put aside under its own name so nobody loses the
        /// evidence, and the backup left by the last good save is tried.
        /// </summary>
        public static Dictionary<string, byte[]> DeserializeSaveFile(string filePath)
        {
            bool whole;
            Dictionary<string, byte[]> savedKeys = ReadFile(filePath, out whole);

            if (whole)
            {
                return savedKeys ?? new Dictionary<string, byte[]>();
            }

            // Not whole, so this file is not to be trusted as the newest save:
            // named aside, so the next save cannot quietly promote it to being
            // the backup.
            string aside = PutAside(filePath);

            if (savedKeys != null && savedKeys.Count > 0)
            {
                UnityEngine.Debug.LogWarning("[Saving] Kept the " + savedKeys.Count + " section(s) of " +
                    Path.GetFileName(filePath) + " that read" +
                    (aside != null ? "; the file itself is now " + Path.GetFileName(aside) : "") + ".");

                return savedKeys;
            }

            Dictionary<string, byte[]> backup = ReadFile(filePath + Backup, out whole);

            if (backup != null && backup.Count > 0)
            {
                UnityEngine.Debug.LogWarning("[Saving] " + Path.GetFileName(filePath) + " could not be read at all, " +
                    "so the backup from the save before it was used" + (whole ? "" : ", and that is damaged too") +
                    ".");

                return backup;
            }

            return new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// Reads every section it can. Whole says whether it reached the end
        /// of the file with everything making sense; null means there was no
        /// file at all.
        /// </summary>
        private static Dictionary<string, byte[]> ReadFile(string filePath, out bool whole)
        {
            whole = false;

            if (!File.Exists(filePath))
            {
                return null;
            }

            byte[] bytesData;

            try
            {
                bytesData = File.ReadAllBytes(filePath);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogWarning("[Saving] " + filePath + " could not be opened: " + e.Message);
                return null;
            }

            Dictionary<string, byte[]> savedKeys = new Dictionary<string, byte[]>();

            using (BinaryReader reader = new BinaryReader(new MemoryStream(bytesData)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    long at = reader.BaseStream.Position;
                    string rKey;
                    int size;

                    try
                    {
                        rKey = reader.ReadString();
                        size = reader.ReadInt32();
                    }
                    catch (System.Exception e)
                    {
                        UnityEngine.Debug.LogWarning("[Saving] " + Path.GetFileName(filePath) + " stops making " +
                            "sense at byte " + at + " of " + bytesData.Length + ": " + e.Message);

                        return savedKeys;
                    }

                    long left = reader.BaseStream.Length - reader.BaseStream.Position;

                    // A length that cannot be true is the end of the real save
                    // and the start of whatever was left lying after it.
                    if (size < 0 || size > left)
                    {
                        UnityEngine.Debug.LogWarning("[Saving] " + Path.GetFileName(filePath) + " says the section " +
                            "at byte " + at + " is " + size + " bytes and only " + left + " remain, so it is read no " +
                            "further. " + savedKeys.Count + " section(s) were good.");

                        return savedKeys;
                    }

                    if (string.IsNullOrWhiteSpace(rKey))
                    {
                        UnityEngine.Debug.LogWarning("[Saving] " + Path.GetFileName(filePath) + " has a section with " +
                            "no name at byte " + at + ", which is what a file written over a longer one looks like. " +
                            "It is read no further; " + savedKeys.Count + " section(s) were good.");

                        return savedKeys;
                    }

                    byte[] rValue = reader.ReadBytes(size);

                    if (savedKeys.ContainsKey(rKey))
                    {
                        UnityEngine.Debug.LogWarning("[Saving] " + Path.GetFileName(filePath) + " holds " + rKey +
                            " twice; the first one is kept.");

                        continue;
                    }

                    savedKeys.Add(rKey, rValue);
                }
            }

            whole = true;

            return savedKeys;
        }

        /// <summary>
        /// Renames a file that could not be read, so it is still there to be
        /// looked at and cannot be mistaken for a good save. Returns where it
        /// went, or null where it could not be moved.
        /// </summary>
        private static string PutAside(string filePath)
        {
            string aside = filePath + "-unreadable-" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss");

            try
            {
                File.Move(filePath, aside);

                return aside;
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogWarning("[Saving] " + filePath + " could not be set aside: " + e.Message);

                return null;
            }
        }

        /// <summary>
        /// Writes a save.
        ///
        /// Written beside the real file and moved into place when it is
        /// finished, so a game that stops or crashes halfway through leaves
        /// either the old save or the new one and never half of each. The save
        /// it replaces is kept as a backup, which is what the reader falls
        /// back to when the newest one cannot be read.
        /// </summary>
        public static void SerializeSaveFile(string filePath, Dictionary<string, byte[]> savedKeys)
        {
            string writing = filePath + Writing;

            using (FileStream file = GetOrCreateFile(writing))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    foreach (KeyValuePair<string, byte[]> pairs in savedKeys)
                    {
                        writer.Write(pairs.Key);
                        writer.Write(pairs.Value.Length);
                        writer.Write(pairs.Value);
                    }

                    writer.Flush();
                }

                // To the disk itself, not only to whatever the system is
                // holding: a machine losing power should not lose the save.
                file.Flush(true);
            }

            try
            {
                if (File.Exists(filePath))
                {
                    string backup = filePath + Backup;

                    if (File.Exists(backup))
                    {
                        File.Delete(backup);
                    }

                    // Two moves rather than a replace: moves are the same on
                    // every platform this runs on, and a crash between them
                    // leaves the save as the backup, which the reader knows to
                    // look for.
                    File.Move(filePath, backup);
                }

                File.Move(writing, filePath);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError("[Saving] " + filePath + " could not be put in place: " + e.Message +
                    ". What was written is at " + Path.GetFileName(writing) + ".");
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