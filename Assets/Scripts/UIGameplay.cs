using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI towers;
    [SerializeField] TextMeshProUGUI state;
    [SerializeField] Player player;
    void Start() {
        GameplayManager.endEnemyAttack += EndUI; 
        GameplayManager.startEnemyAttack += PlayUI;
        Player.TowerCreated += UsedTower;
    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= EndUI;
        GameplayManager.startEnemyAttack -= PlayUI;
        Player.TowerCreated -= UsedTower;
    }

    void PlayUI() {
        state.text = "Defend";
    }
    void EndUI() {
        state.text = "Prepare";
        towers.text = "Towers: " + player.GetActualTowers();
    }
    void UsedTower() {
       towers.text = "Towers: " + player.GetActualTowers();
    }

}
