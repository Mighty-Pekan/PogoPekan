using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaxTimeSetter : MonoBehaviour {

    public int MaxTimeForFish {get => maxTimeForFish;}

    [SerializeField] int maxTimeForFish;
    private void Start() {
        GameController.Instance.RegisterFishTimeSetter(this);
    }

    
}
