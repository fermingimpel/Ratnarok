using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildings : MonoBehaviour {

    public enum TypeOfBuilds {
        Cannon,
        Fence,
        None
    }

    public delegate void UIBuildingButtonPressed(TypeOfBuilds tob);
    public static event UIBuildingButtonPressed BuildingButtonPressed;

    [SerializeField] GameObject UIWheel;
    [SerializeField] GameObject backButton;
   
    private void Start() {
        BuildingCreator.ClickedBase += ClickedBase;
    }
    private void OnDestroy() {
        BuildingCreator.ClickedBase -= ClickedBase;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UIWheel.SetActive(false);
        backButton.SetActive(false);
        }
    }

    void ClickedBase() {
        UIWheel.SetActive(true);
        backButton.SetActive(true);
    }
    public void ClickBackButton() {
        UIWheel.SetActive(false);
        backButton.SetActive(false);
    }
    public void PressButtonStructure(int button) {
        if (BuildingButtonPressed != null) 
            BuildingButtonPressed((TypeOfBuilds)(button));
        UIWheel.SetActive(false);
        backButton.SetActive(false);
    }

}
