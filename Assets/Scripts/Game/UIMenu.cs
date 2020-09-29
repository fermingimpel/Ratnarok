using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour {
   [SerializeField] GameObject UIConfig;
    bool configDisplayed = false;

   void Start() {

    }
    void Update() {

    }

    public void ClickedOptions() {
        configDisplayed = !configDisplayed;
        if(configDisplayed) {
            UIConfig.SetActive(true);
            return;
        }

        UIConfig.SetActive(false);
        return;
    }

}
