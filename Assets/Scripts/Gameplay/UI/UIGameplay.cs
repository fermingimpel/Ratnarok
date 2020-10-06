using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Image hordeRat;
    [SerializeField] Image hpBar;

    [SerializeField] GameObject go1;
    [SerializeField] GameObject go2;

    void Start() {
        BuildingCreator.ChangedTools += ChangeTools;
        GameplayManager.UpdateBarHorde += ChangeHordeBar;
        Town.ChangedHP += ChangeHP;
    }
    private void OnDisable() {
        BuildingCreator.ChangedTools -= ChangeTools;
        GameplayManager.UpdateBarHorde -= ChangeHordeBar;
        Town.ChangedHP -= ChangeHP;
    }
    void ChangeTools(int t) {
        goldText.text = t.ToString();
    }
    void ChangeHP(float hp, float maxHP) {
        hpBar.fillAmount = hp / maxHP;
    }
    void ChangeHordeBar(float timeInHorde, float maxTimeInHorde) {
        hordeRat.transform.position = Vector3.Lerp(go1.transform.position, go2.transform.position, timeInHorde / maxTimeInHorde);
    }
}
