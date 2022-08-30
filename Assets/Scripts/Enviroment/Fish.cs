using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public enum FishId {ONE = 1,TWO =2};
    [SerializeField] FishId id = FishId.ONE;

    private void Start() {
        if(LevelsDataManager.Instance.IsFishFound(id))Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        LevelsDataManager.Instance.SetFishFound(id);
        Destroy(gameObject);
    }

}
