using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Image hordeRat;
    [SerializeField] Image hordeBar;
    [SerializeField] GameObject hordeGO;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject hpGO;

    [SerializeField] GameObject startText;
    [SerializeField] GameObject prepareText;

    [SerializeField] GameObject go1;
    [SerializeField] GameObject go2;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject rataryMenu;
    [SerializeField] GameObject configMenu;
    [SerializeField] GameObject[] configOpen;
    bool configDisplayed = true;

    public static Action ClickedPause;
    public static Action ClickedResume;

    void Start() {
        hordeGO.SetActive(false);
        hpGO.SetActive(false);
        startText.SetActive(false);
        GameplayManager.StartEnemyAttack += StartGame;
        GameplayManager.StartPreAtk += PreStart;
        BuildingCreator.ChangedTools += ChangeTools;
        GameplayManager.UpdateBarHorde += ChangeHordeBar;
        Town.ChangedHP += ChangeHP;
    }
    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartGame;
        GameplayManager.StartPreAtk -= PreStart;
        BuildingCreator.ChangedTools -= ChangeTools;
        GameplayManager.UpdateBarHorde -= ChangeHordeBar;
        Town.ChangedHP -= ChangeHP;
    }
    void StartGame() {
        hpGO.SetActive(true);
        hordeGO.SetActive(true);
    }
    void PreStart() {
        StartCoroutine(PreStartGame());
    }
    IEnumerator PreStartGame() {
        prepareText.SetActive(false);
        startText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        startText.SetActive(false);
        StopCoroutine(PreStartGame());
        yield return null;

    }

    void ChangeTools(float t) {
        goldText.text = t.ToString();
    }
    void ChangeHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeHordeBar(float timeInHorde, float maxTimeInHorde) {
        hordeRat.transform.position = Vector3.Lerp(go1.transform.position, go2.transform.position, timeInHorde / maxTimeInHorde);
        hordeBar.fillAmount = timeInHorde / maxTimeInHorde;
    }

    public void ClickedPauseButton() {
        if (ClickedPause != null)
            ClickedPause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ClickedResumeButton() {
        if (ClickedResume != null)
            ClickedResume();
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
