using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour {
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Image hordeRat;
    [SerializeField] Image hpBar1;
    [SerializeField] Image hpBar2;

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
        goldText.text = "Tools: " + t;
    }
    void ChangeHP(float hp, float maxHP) {
        hpBar1.fillAmount = hp / maxHP;
        hpBar2.fillAmount = hp / maxHP;
    }
    void ChangeHordeBar(float timeInHorde, float maxTimeInHorde) {
        hordeRat.transform.position = Vector3.Lerp(go1.transform.position, go2.transform.position, timeInHorde / maxTimeInHorde);
    }
}
