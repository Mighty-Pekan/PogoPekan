using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsDataManager : MonoSingleton<LevelsDataManager>
{
    [SerializeField] bool resetDB = false;
    [SerializeField] bool unlockAllLevels = false;

    private List<LevelData> levelsData;
    private const string DB_FILE_NAME = "db.json";


    private void Start() {

        levelsData = FileHandler.ReadListFromJSON<LevelData>(DB_FILE_NAME);

        if (levelsData == null || resetDB ) {
            Debug.Log("creating new db");
            CreateNewDB();
        }
        else Debug.Log("existing db loaded");
    }

    public bool isLevelUnlocked(int world, int level) {

        if (world <= 0) throw new Exception("world index < 1");
        if(level <=0) throw new Exception("level < 1");

        return levelsData[(GameController.Instance.NumLevelsPerWorld * (world-1)) + level-1].Unlocked;
    }

    private void CreateNewDB() {
        levelsData = new List<LevelData>();

        for (int i = 0; i < GameController.Instance.NumWorlds; i++) {
            for (int j = 0; j < GameController.Instance.NumLevelsPerWorld; j++) {
                levelsData.Add(new LevelData(i, j));
            }
        }
        FileHandler.SaveToJSON<LevelData>(levelsData, DB_FILE_NAME);
    }
}

[Serializable]
public class LevelData {
    public int World;
    public int Level;
    public bool Unlocked;
    public bool[] Fishes;

    public LevelData(int _world, int _level) {
        World = _world;
        Level = _level;
        Unlocked = false;
        Fishes = new bool[3] { false, false, false };
    }
}
