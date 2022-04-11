using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonFileSystem<T> where T : new()
{
    public static void Load(string directory, string file, out T t)
    {
        string dirPath = $"{Application.persistentDataPath}/{directory}";
        string filePath = $"{dirPath}/{file}";

        // if there's no file, make new file
        if (!Directory.Exists(dirPath) || !File.Exists(dirPath))
        {
            t = new T();
            Save(directory, file, t);
        }

        // if not, read file and load data
        else
        {
            t = JsonUtility.FromJson<T>(File.ReadAllText(filePath));
        }
    }

    public static void Save(string directory, string file, T t)
    {
        string dirPath = $"{Application.persistentDataPath}/{directory}";
        string filePath = $"{dirPath}/{file}";

        // if there's no directory create it newly
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

        // save file in the directory
        File.WriteAllText(filePath, JsonUtility.ToJson(t));
    }
}
