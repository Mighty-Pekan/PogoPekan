using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    protected static T instance;

    protected virtual void Awake() {
        if (instance)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            instance = (T)this;
        }
    }

}
