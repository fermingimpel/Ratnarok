using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInformationBuilds : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBuildHP;
    [SerializeField] TextMeshProUGUI textBuildDamage;
    [SerializeField] TextMeshProUGUI textBuildAS;
    
    Vector3 offset = new Vector3(75, 0, 0);

    private void Start() {
        this.gameObject.SetActive(false);
        BuildingCreator.BuildClicked += ClickedStructure;
    }

    private void OnDestroy() {
        BuildingCreator.BuildClicked -= ClickedStructure;
    }

    public void ClickedStructure(Build b, Vector3 mousePos) {
        transform.position = mousePos + offset;

        textBuildHP.text = "HP: " + b.GetHP();
        textBuildAS.text = "AS: " + b.GetAttackSpeed();
        textBuildDamage.text = "DMG: " + b.GetDamage();
    }


}
