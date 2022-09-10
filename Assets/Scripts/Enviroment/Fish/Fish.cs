using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : GrayFish
{
    public enum FishId {ONE = 1,TWO =2};
    [SerializeField] FishId id = FishId.ONE;

    [SerializeField] GameObject GrayFishPrefab;

    private void Start() {
        if (LevelsDataManager.Instance.IsFishFound(id)) {
            Instantiate(GrayFishPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        LevelsDataManager.Instance.SetFishFound(id);
        base.OnTriggerEnter2D(collision);
    }

}
