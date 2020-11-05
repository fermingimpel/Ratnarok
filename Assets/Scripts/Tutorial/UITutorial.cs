using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject backgroundUIWheel;
    [SerializeField] GameObject UIWheel;
   // [SerializeField] GameObject continueText;
    public delegate void UIBuildTutorialPressed();
    public static event UIBuildTutorialPressed BuildTutorialPressed;

    public static Action ClickedRatary;

    [SerializeField] Image wilfredImage;
    [SerializeField] Image wilfredText;
    [SerializeField] Sprite[] wilfredTextsOrder;
    [SerializeField] Sprite[] wilfredImagesOrder;
    [SerializeField] Canvas canvas;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject rataryMenu;

    private void Start() {
        Time.timeScale = 1.0f;
        BuildCreatorTutorial.ChangedTools += ChangeGold;
        BuildCreatorTutorial.ClickedBase += ClickedBase;
        Town.ChangedHP += ChangedHP;
        TutorialManager.TutorialPhaseChanged += ChangeWilfredText;
        TutorialManager.TutorialPhaseChanged += CheckTutorialPhase;
    }
    private void OnDisable() {
        BuildCreatorTutorial.ChangedTools -= ChangeGold;
        BuildCreatorTutorial.ClickedBase  -= ClickedBase;
        Town.ChangedHP -= ChangedHP;
        TutorialManager.TutorialPhaseChanged -= ChangeWilfredText;
        TutorialManager.TutorialPhaseChanged -= CheckTutorialPhase;
    }

    public void ClickedPause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void ClickRatary() {
        rataryMenu.SetActive(true);
        Time.timeScale = 0f;
        if (ClickedRatary != null)
            ClickedRatary();
    }
    public void ClickedResumeButton() {
        pauseMenu.SetActive(false);
        rataryMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void ClickedBase() {
        backgroundUIWheel.SetActive(true);
        UIWheel.SetActive(true);
        UIWheel.transform.position = CheckScreenLimits(Input.mousePosition);
    }
    Vector3 CheckScreenLimits(Vector3 actualPos) {
        Image wheelImage = UIWheel.GetComponent<Image>();
        if (actualPos.y > Screen.height - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f)
            actualPos = new Vector3(actualPos.x, Screen.height - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f, actualPos.z);
        else if (actualPos.y < wheelImage.sprite.rect.height * canvas.scaleFactor * 0.5f)
            actualPos = new Vector3(actualPos.x, wheelImage.sprite.rect.height * canvas.scaleFactor * 0.5f, actualPos.z);

        if (actualPos.x < wheelImage.sprite.rect.width * canvas.scaleFactor * 0.5f)
            actualPos = new Vector3(wheelImage.sprite.rect.width * canvas.scaleFactor * 0.5f, actualPos.y, actualPos.z);
        else if (actualPos.x > Screen.width - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f)
            actualPos = new Vector3(Screen.width - (wheelImage.sprite.rect.width * canvas.scaleFactor) * 0.5f, actualPos.y, actualPos.z);

        return actualPos;
    }
    void ClickedBGWheel() {
        UIWheel.SetActive(false);
        backgroundUIWheel.SetActive(false);
    }

    public void PressButtonTurret() {
        if (BuildTutorialPressed != null)
            BuildTutorialPressed();
        UIWheel.SetActive(false);
        backgroundUIWheel.SetActive(false);
    }

    void ChangedHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeGold(float g) {
        goldText.text = g.ToString();
    }
    void CheckTutorialPhase(int p) {

    }
    void ChangeWilfredText(int ind) {
        wilfredText.sprite = wilfredTextsOrder[ind];
        wilfredText.SetNativeSize();
        wilfredImage.sprite = wilfredImagesOrder[ind];
        wilfredImage.SetNativeSize();
    }
}
