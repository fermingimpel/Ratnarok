using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsGameplay : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject rataryMenu;
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

}
