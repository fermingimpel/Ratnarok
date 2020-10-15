using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI wilfredText;
    [SerializeField] List<string> wilfredTextsString;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject UIWheel;
    [SerializeField] GameObject continueText;
    public delegate void UIBuildTutorialPressed();
    public static event UIBuildTutorialPressed BuildTutorialPressed;

    private void Start() {
        wilfredText.text = wilfredTextsString[0];
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

    void ClickedBase() {
        UIWheel.SetActive(true);
    }

    public void PressButtonTurret() {
        if (BuildTutorialPressed != null)
            BuildTutorialPressed();
        UIWheel.SetActive(false);
    }

    void ChangedHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeGold(int g) {
        goldText.text = g.ToString();
    }
    void CheckTutorialPhase(int p) {
        if (p == 0 || p == 1 || p == 4)
            continueText.SetActive(true);
        else
            continueText.SetActive(false);
    }
    void ChangeWilfredText(int ind) {
        wilfredText.text = wilfredTextsString[ind];
    }
}
