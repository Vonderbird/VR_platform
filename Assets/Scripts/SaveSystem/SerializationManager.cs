﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace saveSystem
{

    public class SerializationManager
    {
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }
            // ema stands for "eat my ass"
            string path = Application.persistentDataPath + "/saves/" + saveName + ".ema";
            FileStream file = File.Create(path);
            
            formatter.Serialize(file, saveData);
            
            file.Close();

            return true;
        }

        public static object Load(string path)
        {
            if (!File.Exists(path))
                return null;

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to lead file at {0}", path);
                file.Close();
                return null;
            }
        }
        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            SurrogateSelector selector = new SurrogateSelector();
            
            Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
            QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();
            
            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), vector3Surrogate);

            formatter.SurrogateSelector = selector;
            
            return formatter;
        }
        
    }
    
}// saveSystem