﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildings : MonoBehaviour {

    public enum TypeOfBuilds {
        Cannon,
        ToolsGenerator,
        Fence,
        None
    }

    public delegate void UIBuildingButtonPressed(TypeOfBuilds tob);
    public static event UIBuildingButtonPressed BuildingButtonPressed;

    public void PressButtonStructure(int button) {
        Debug.Log(button);
        Debug.Log((TypeOfBuilds)(button));
        if (BuildingButtonPressed != null) 
            BuildingButtonPressed((TypeOfBuilds)(button));
    }

}
