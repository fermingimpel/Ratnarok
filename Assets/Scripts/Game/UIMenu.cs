using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenu : MonoBehaviour {
   [SerializeField] GameObject UIConfig;
    bool configDisplayed = false;
    [SerializeField] TextMeshProUGUI textVersion;

    [SerializeField] GameObject[] configOpen;
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
    public void ClickedToggleScreenType() {
        for(int i=0;i<configOpen.Length;i++)
            if (configOpen[i] != null) {
                if (configOpen[i].activeSelf)
                    configOpen[i].SetActive(false);
                else
                    configOpen[i].SetActive(true);
            }
    }
}