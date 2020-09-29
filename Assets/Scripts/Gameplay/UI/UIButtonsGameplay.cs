using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsGameplay : MonoBehaviour {
    [SerializeField] GameObject pauseMenu;

    public void ClickedPauseButton() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ClickedResumeButton() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
