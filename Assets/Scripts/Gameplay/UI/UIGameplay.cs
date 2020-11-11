using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
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
    bool gamePaused = false;
    void Awake() {
        hpGO.SetActive(false);
        startText.SetActive(false);
        GameplayManager.StartEnemyAttack += StartGame;
        GameplayManager.StartPreAtk += PreStart;
        BuildingCreator.ChangedGold += ChangedGold;
        EnemyManager.HordeUpdate += ChangeHordeBar;
        Town.ChangedHP += ChangeHP;
        gamePaused = false;
    }
    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartGame;
        GameplayManager.StartPreAtk -= PreStart;
        BuildingCreator.ChangedGold -= ChangedGold;
        EnemyManager.HordeUpdate -= ChangeHordeBar;
        Town.ChangedHP -= ChangeHP;
    }
    void StartGame() {
        hpGO.SetActive(true);
    }
    void PreStart() {
        StartCoroutine(PreStartGame());
    }
    IEnumerator PreStartGame() {
        prepareText.SetActive(false);
        startText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        startText.SetActive(false);
        hordeGO.SetActive(true);
        yield return null;

    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!gamePaused)
                ClickedPauseButton();
            else
                ClickedResumeButton();
        }
    }
    void ChangedGold(float g) {
        goldText.text = g.ToString();
    }
    void ChangeHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeHordeBar(int actualHorde, int maxHorde) {
        hordeText.text = "HORDE: " + actualHorde + " | " + maxHorde;
    }

    public void ClickedPauseButton() {
        if (ClickedPause != null)
            ClickedPause();
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ClickedResumeButton() {
        if (ClickedResume != null)
            ClickedResume();
        gamePaused = false;
        pauseMenu.SetActive(false);
        rataryMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ClickedRatary() {
        if (ClickedPause != null)
            ClickedPause();
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
