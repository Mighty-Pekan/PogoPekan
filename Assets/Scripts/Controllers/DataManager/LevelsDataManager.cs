using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Fish;

public class LevelsDataManager : MonoSingleton<LevelsDataManager>
{
    [SerializeField] bool resetDB = false;

    private List<LevelData> levelsData;
    private const string DB_FILE_NAME = "db.json";


    protected override void Awake() {
        base.Awake();   
        LoadData();
        if (isDBNull() || resetDB ) {
            Debug.Log("creating new db");
            CreateNewDB();
        }
        else Debug.Log("existing db loaded");
    }
    //==============================================================================setter
    public void UnlockLevel(int world, int level) {
        if (world <= 0) throw new Exception("world index < 1");
        if (level <= 0) throw new Exception("level < 1");

        if(!GetLevelData(world,level).Unlocked) {
            GetLevelData(world,level).Unlocked = true;
            SaveData();
        }
    }

    public void SetFishFound(Fish.FishId fishId) {
        Debug.Log("set fish found: "+(int)fishId);  
        int [] currentLevel = GameController.Instance.GetCurrentLevel();
        GetLevelData(currentLevel[0], currentLevel[1]).Fishes[(int)fishId] = true;
        SaveData();
    }

    public void SetTimeFishFound() {
        int[] currentLevel = GameController.Instance.GetCurrentLevel();
        GetLevelData(currentLevel[0], currentLevel[1]).Fishes[0] = true;
        SaveData();
    }

    public void RegisterNewTime(int newTime) {
        int[] currentLevel = GameController.Instance.GetCurrentLevel();
        int oldTime = GetLevelData(currentLevel[0], currentLevel[1]).BestTime;
        if (oldTime == -1 || newTime < oldTime) {
            GetLevelData(currentLevel[0], currentLevel[1]).BestTime = newTime;
        }
        SaveData();
    }

    //==============================================================================getter
    public bool IsLevelUnlocked(int world, int level) {
        if (world <= 0) throw new Exception("world index < 1");
        if (level <= 0) throw new Exception("level < 1");
        return GetLevelData(world, level).Unlocked;
    }
    public bool IsFishFound(Fish.FishId fishId) {
        int[] currentLevel = GameController.Instance.GetCurrentLevel();
        return GetLevelData(currentLevel[0], currentLevel[1]).Fishes[(int)fishId];
    }

    public int GetNumFishFound(int world,int level) {
        int ris = 0;
        bool[] fishesFound = GetLevelData(world, level).Fishes;
        for (int i = 0; i < fishesFound.Length; i++) if (fishesFound[i]) ris++;
        return ris;
    }

    public int GetBestTime(int world, int level) {
        return GetLevelData(world, level).BestTime;
    }

    // ----------------------------------------------------------------------------------------------------------private methods
    private void CreateNewDB() {
        levelsData = new List<LevelData>();

        for (int i = 0; i < GameController.Instance.NumWorlds; i++) {
            for (int j = 0; j < GameController.Instance.NumLevelsPerWorld; j++) {
                levelsData.Add(new LevelData(i, j));
            }
        }
        levelsData[0].Unlocked = true;
        FileHandler.SaveToJSON<LevelData>(levelsData, DB_FILE_NAME);
    }
    private void LoadData() {
        levelsData = FileHandler.ReadListFromJSON<LevelData>(DB_FILE_NAME);
    }
    private void SaveData() {
        FileHandler.SaveToJSON<LevelData>(levelsData, DB_FILE_NAME);
    }
    private LevelData GetLevelData(int world, int level) {
        //Debug.Log("get level data called with world: "+ world + " level: "+level);
        return levelsData[(GameController.Instance.NumLevelsPerWorld * (world - 1)) + level - 1];
    }
    private bool isDBNull() { return levelsData == null; }

    [Serializable]
    private class LevelData {
        public int World;
        public int Level;
        public bool Unlocked;
        public bool[] Fishes;
        public int BestTime;

        public LevelData(int _world, int _level) {
            World = _world;
            Level = _level;
            Unlocked = false;
            Fishes = new bool[3] { false, false, false };
            BestTime = -1;
        }
    }
}

//[Serializable]
//private class LevelData {
//    public int World;
//    public int Level;
//    public bool Unlocked;
//    public bool[] Fishes;

//    public LevelData(int _world, int _level) {
//        World = _world;
//        Level = _level;
//        Unlocked = false;
//        Fishes = new bool[3] { false, false, false };
//    }
//}
