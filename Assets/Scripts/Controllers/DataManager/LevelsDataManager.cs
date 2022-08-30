using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsDataManager : MonoBehaviour
{

    public List<LevelData> levelsData;

    private void Start() {
        levelsData = FileHandler.ReadListFromJSON<LevelData>("prova.json");

        if (levelsData == null) {
            Debug.Log("levels data is empty, creating a new one");
            levelsData = new List<LevelData>();

            for (int i = 0; i < GameController.Instance.NumWorlds; i++) {
                for (int j = 0; j < GameController.Instance.NumLevelsPerWorld; j++) {
                    levelsData.Add(new LevelData(i, j));
                }
            }
        }
        else Debug.Log("levels data already saved");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) FileHandler.SaveToJSON<LevelData>(levelsData, "prova.json");
        if (Input.GetKeyDown(KeyCode.L)) FileHandler.ReadListFromJSON<LevelData>("prova.json");
    }
}

[Serializable]
public class LevelData {
    public int World;
    public int Level;
    public bool Unlocked;
    //public bool[] Fishes;

    public LevelData(int _world, int _level) {
        World = _world;
        Level = _level;
        Unlocked = false;
        //Fishes = new bool[3] { false, false, false };
    }
}
