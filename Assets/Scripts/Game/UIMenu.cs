using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenu : MonoBehaviour {
   [SerializeField] GameObject UIConfig;
    bool configDisplayed = false;
    [SerializeField] TextMeshProUGUI textVersion;

   void Start() {
        textVersion.text = "V: " + Application.version;
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