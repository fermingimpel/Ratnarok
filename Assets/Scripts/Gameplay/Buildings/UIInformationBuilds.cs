using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInformationBuilds : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBuildHP;
    [SerializeField] TextMeshProUGUI textBuildDamage;
    [SerializeField] TextMeshProUGUI textBuildAS;
    [SerializeField] Build build;
    
    Vector3 offset = new Vector3(75, 0, 0);

    private void Start() {
        this.gameObject.SetActive(false);
        BuildingCreator.BuildClicked += ClickedStructure;
    }

    private void OnDestroy() {
        BuildingCreator.BuildClicked -= ClickedStructure;
    }

    private void Update() {
        if (build == null) {
            if (this.gameObject.activeSelf)
                this.gameObject.SetActive(false);
        }
    }

    public void ClickedStructure(Build b, Vector3 mousePos) {
        build = b;
        transform.position = mousePos + offset;
        UpdateUI();
    }

    void UpdateUI() {
        if (build != null) {
            textBuildHP.text = "HP: " + build.GetHP();
            textBuildAS.text = "AS: " + build.GetAttackSpeed();
            textBuildDamage.text = "DMG: " + build.GetDamage();
        }
    }

    public void ClickRepairButton() {
        if (build != null) {
            build.RepairBuild();
            UpdateUI();
        }
    }


}
