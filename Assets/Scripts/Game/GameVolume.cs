using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVolume : MonoBehaviour {
    public float MusicVolume;
    public float SFXVolume;

    static GameVolume gv;
    void Awake() {
        if (gv != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
