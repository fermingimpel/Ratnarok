using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildings : MonoBehaviour {

    public enum TypeOfBuilds {
        None,
        Tower,
        KnivesSpinner
    }

    public delegate void UIBuildingButtonPressed(TypeOfBuilds tob);
    public static event UIBuildingButtonPressed BuildingButtonPressed;

    public void TowerButton() {
        if (BuildingButtonPressed != null)
            BuildingButtonPressed(TypeOfBuilds.Tower);
    }

    public void KnivesSpinnerButton() {
        if (BuildingButtonPressed != null)
            BuildingButtonPressed(TypeOfBuilds.KnivesSpinner);
    }

}
