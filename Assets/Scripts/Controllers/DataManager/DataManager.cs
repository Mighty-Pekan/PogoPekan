using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    private LevelsData levelsData;

    private string path = "";
    private string persistentPath = "";

    // Start is called before the first frame update
    void Start() {
        levelsData = new LevelsData();
        SetPaths();
    }

    private void SetPaths() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) SaveData();
        if (Input.GetKeyDown(KeyCode.L)) LoadData();
    }

    public void SaveData() {
        Debug.Log("Data manager: Saving Data at " + persistentPath);

        string json = JsonUtility.ToJson(levelsData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
    }

    public void LoadData() {
        Debug.Log("Data manager: loading data");
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        LevelsData data = JsonUtility.FromJson<LevelsData>(json);
        Debug.Log(data.ToString());
    }

}

public class LevelData {
    public int World;
    public int Level;
    public bool Unlocked;
    public bool[] Fishes;

    public LevelData(int _world, int _level) {
        World = _world;
        Level = _level;
        Unlocked = false;
        Fishes = new bool[3]{false,false,false}; 
    }
    public override string ToString() {
        return "data bla bla";
    }
}

public class LevelsData{

    public LevelData[,] levelsData;

    public LevelsData() {
        levelsData = new LevelData[GameController.Instance.NumWorlds, GameController.Instance.NumLevelsPerWorld];

        for (int i = 0; i < levelsData.GetLength(0); i++) {
            for (int j = 0; j < levelsData.GetLength(1); j++) {
                levelsData[i, j] = new LevelData(1, 1);
            }
        }

    }

    public override string ToString() {
        string ris = "";
        for(int i = 0; i < levelsData.GetLength(0); i++) {
            for(int j = 0; j < levelsData.GetLength(1); j++) {
                ris += (levelsData[i,j].ToString() + "\n");
            }
        }
        return ris;
    }

}
