using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] Image hordeBar;
    void Start() {
        BuildingCreator.ChangedTools += ChangeTools;
        GameplayManager.UIStateUpdate += ChangeState;
        GameplayManager.UpdateBarHorde += ChangeHordeBar;
    }

    private void OnDisable() {
        BuildingCreator.ChangedTools -= ChangeTools;
        GameplayManager.UIStateUpdate -= ChangeState;
        GameplayManager.UpdateBarHorde -= ChangeHordeBar;
    }
    void ChangeTools(int t) {
        goldText.text = "Tools: " + t;
    }
    void ChangeHordeBar(float timeInHorde, float maxTimeInHorde) {
        hordeBar.fillAmount = timeInHorde / maxTimeInHorde;
    }
    void ChangeState(GameplayManager.Stage s) {
        switch (s) {
            case GameplayManager.Stage.Attack:
                progressText.text = "DEFENDING...";
                break;
            case GameplayManager.Stage.Preparing:
                progressText.text = "PREPARING...";
                break;
            default:
                break;
        }
    }
}
