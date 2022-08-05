using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            Instance = (T)this;
        }
    }
}
