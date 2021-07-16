using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem : MonoBehaviour
{




    public static void Save<T>(T ObjectToSave, string key)
    {
        string path = Application.persistentDataPath + "/Save/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;

        using (FileStream fileStream = new FileStream(path + key + ".txt",FileMode.Create))
        {
            formatter.Serialize(fileStream, ObjectToSave);
        }

        Debug.Log("Saved");
    }

    public static T Load<T>( string key)
    {
        string path = Application.persistentDataPath + "/Save/";
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;

        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt", FileMode.Open))
        {
          returnValue =  (T)formatter.Deserialize(fileStream);
        }

        Debug.Log("Loaded");

        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + "/Save/" + key + ".txt";
        return File.Exists(path);
    }

    public static void DeleteAllSave()
    {
        string path = Application.persistentDataPath + "/Save/";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
        Directory.CreateDirectory(path);

    }
}
