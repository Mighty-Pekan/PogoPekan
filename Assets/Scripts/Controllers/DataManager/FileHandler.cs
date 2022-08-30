using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine.WSA;
using UnityEngine;

public static class FileHandler {

    /// <summary>
    /// saves list of objects to json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toSave"></param>
    /// <param name="filename"></param>
    public static void SaveToJSON<T>(List<T> toSave, string filename) {
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(filename), content);
        Debug.Log("SAVED: " + content + "\n" + "to: " + GetPath(filename));
    }
    /// <summary>
    /// saves object to json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toSave"></param>
    /// <param name="filename"></param>
    public static void SaveToJSON<T>(T toSave, string filename) {
        string content = JsonUtility.ToJson(toSave);
        WriteFile(GetPath(filename), content);
        Debug.Log("SAVED: " + content + "\n"+"to: " + GetPath(filename));
    }

    public static List<T> ReadListFromJSON<T>(string filename) {
        string content = ReadFile(GetPath(filename));

        if (string.IsNullOrEmpty(content) || content == "{}") {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        Debug.Log("LOADED: " + content + "\n" + "from: " + GetPath(filename));
        return res;
    }

    public static T ReadFromJSON<T>(string filename) {
        string content = ReadFile(GetPath(filename));

        if (string.IsNullOrEmpty(content) || content == "{}") {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);

        Debug.Log("LOADED: " + content + "\n" + "from: " + GetPath(filename));
        return res;

    }

    //----------------------------------------------------------------------------------private methods
    private static string GetPath(string filename) {
        return UnityEngine.Application.persistentDataPath + "/" + filename;
    }

    private static void WriteFile(string path, string content) {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path) {
        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}

/// <summary>
/// helper class that allows lists saving
/// </summary>
public static class JsonHelper {
    public static T[] FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}

