using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {
    void Start() {
        AkSoundEngine.SetSwitch("General_music", "menu_general", this.gameObject);
        AkSoundEngine.SetSwitch("Menu", "menu", this.gameObject);
        AkSoundEngine.SetState("menu_scene", "menu");

        AkSoundEngine.PostEvent("game_start", this.gameObject);
    }

    bool rataryDisplayed = false;
    [SerializeField] GameObject rataryGO;

    public void ClickRatary() {
        rataryDisplayed = !rataryDisplayed;

        if (rataryDisplayed) {
            rataryGO.SetActive(true);
            return;
        }
        rataryGO.SetActive(false);
    }

}
