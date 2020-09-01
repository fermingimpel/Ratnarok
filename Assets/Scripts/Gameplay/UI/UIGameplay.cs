using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI stateText;
    void Start() {
        BuildingCreator.ChangedGold += ChangeGold;
        GameplayManager.UIStateUpdate += ChangeState;
        Town.ChangedHP += ChangeHealth;
    }

    private void OnDisable() {
        BuildingCreator.ChangedGold -= ChangeGold;
        GameplayManager.UIStateUpdate -= ChangeState;
        Town.ChangedHP -= ChangeHealth;
    }
    void ChangeGold(int gold) {
        goldText.text = "Gold: " + gold;
    }
    void ChangeHealth(int health) {
        hpText.text = "HP: " + health;
    }
    void ChangeState(GameplayManager.Stage s) {
        switch (s) {
            case GameplayManager.Stage.Attack:
                stateText.text = "Defending";
                break;
            case GameplayManager.Stage.Preparing:
                stateText.text = "Preparing";
                break;
            default:
                break;
        }
    }
}
