using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenu : MonoBehaviour {
   [SerializeField] GameObject UIConfig;
    bool configDisplayed = false;
    [SerializeField] TextMeshProUGUI textVersion;

    [SerializeField] GameObject[] configOpen;

    [SerializeField] GameObject credits;
    bool creditsDisplayed = false;
    void Start() {
        textVersion.text = "V: " + Application.version;

        AkSoundEngine.SetSwitch("General_music", "menu_general", this.gameObject);
        AkSoundEngine.SetSwitch("Menu", "menu", this.gameObject);
        AkSoundEngine.SetState("menu_scene", "menu");
        AkSoundEngine.PostEvent("game_start", this.gameObject);

        //
        // AkSoundEngine.SetState("menu_scene", "menu");
        // AkSoundEngine.PostEvent("game_start", this.gameObject);
    }
    public void ClickCredits() {
        creditsDisplayed = !creditsDisplayed;
        //AkSoundEngine.PostEvent("click_credits", this.gameObject);
        AkSoundEngine.PostEvent("button_generic", this.gameObject);
        if (creditsDisplayed) {
            credits.SetActive(true);
            AkSoundEngine.SetSwitch("Menu", "credits", this.gameObject);
            AkSoundEngine.SetState("menu_scene", "credits");
            return;
        }
        AkSoundEngine.SetSwitch("Menu", "menu", this.gameObject);
        AkSoundEngine.SetState("menu_scene", "menu");
        credits.SetActive(false); ;
    }
    public void ClickedOptions() {
        configDisplayed = !configDisplayed;
        AkSoundEngine.PostEvent("button_generic", this.gameObject);
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