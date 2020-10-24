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

    void ChangeTools(int t) {
        goldText.text = t.ToString();
    }
    void ChangeHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeHordeBar(float timeInHorde, float maxTimeInHorde) {
        hordeRat.transform.position = Vector3.Lerp(go1.transform.position, go2.transform.position, timeInHorde / maxTimeInHorde);
        hordeBar.fillAmount = timeInHorde / maxTimeInHorde;
    }
}
