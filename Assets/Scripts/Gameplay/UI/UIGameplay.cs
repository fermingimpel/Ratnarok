using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] GameObject winScreenUI;
    [SerializeField] GameObject loseScreenUI;

    [SerializeField] TextMeshProUGUI cheeseText;
    [SerializeField] TextMeshProUGUI hordeText;
    [SerializeField] GameObject hordeGO;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject hpGO;

    [SerializeField] GameObject startText;
    [SerializeField] GameObject prepareText;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject rataryMenu;
    [SerializeField] GameObject configMenu;
    [SerializeField] GameObject[] configOpen;
    bool configDisplayed = true;

    public static Action ClickedPause;
    public static Action ClickedResume;
    private void Start() {
        hpGO.SetActive(false);
        startText.SetActive(false);

        Town.ChangedHP += ChangeHP;
        EnemyManager.HordeUpdate += ChangeHordeBar;
    }

    private void OnDisable() {
        Town.ChangedHP -= ChangeHP;
        EnemyManager.HordeUpdate -= ChangeHordeBar;
    }

    public void PreStartGameUI() {
        prepareText.SetActive(false);
        startText.SetActive(true);
    }

    public void StartGameUI() {
        hpGO.SetActive(true);
        startText.SetActive(false);
        hordeGO.SetActive(true);
    }

    public void ActivateLoseScreen() {
        winScreenUI.SetActive(false);
        loseScreenUI.SetActive(true);
    }
    public void ActivateWinScreen() {
        loseScreenUI.SetActive(false);
        winScreenUI.SetActive(true);
    }
    public void ChangeCheese(float c) {
        cheeseText.text = ((int)c).ToString();
    }

    public void ClickPauseButton() {
        if (ClickedPause != null)
            ClickedPause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ClickResumeButton() {
        if (ClickedResume != null)
            ClickedResume();
        pauseMenu.SetActive(false);
        rataryMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ClickRatary() {
        if (ClickedPause != null)
            ClickedPause();
        rataryMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ClickOptions() {
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

    void ChangeHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }

    void ChangeHordeBar(int actualHorde, int maxHorde) {
        hordeText.text = "HORDE: " + actualHorde + " | " + maxHorde;
    }
}
