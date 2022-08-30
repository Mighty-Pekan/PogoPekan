using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaxTimeSetter : MonoBehaviour {

    public int MaxTimeForFish {get => maxTimeForFish;}

    [SerializeField] int maxTimeForFish;
    private void Awake() {
        GameController.Instance.RegisterFishTimeSetter(this);
    }

    
}
