using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsGameplay : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject rataryMenu;
    [SerializeField] GameObject configMenu;
    [SerializeField] GameObject[] configOpen;
    bool configDisplayed=true;

    public void ClickedPauseButton() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ClickedResumeButton() {
        pauseMenu.SetActive(false);
        rataryMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ClickedRatary() {
        rataryMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClickedOptions() {
        configDisplayed = !configDisplayed;
        if (!configDisplayed) {
            pauseMenu.SetActive(false);
            configMenu.SetActive(true);
            return;
        }

        pauseMenu.SetActive(true);
        configMenu.SetActive(false);
        return;
    }

    public void ClickedToggleScreenType() {
        for (int i = 0; i < configOpen.Length; i++)
            if (configOpen[i] != null) {
                if (configOpen[i].activeSelf)
                    configOpen[i].SetActive(false);
                else
                    configOpen[i].SetActive(true);
            }
    }
}
