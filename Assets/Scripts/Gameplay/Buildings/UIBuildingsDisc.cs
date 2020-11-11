using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingsDisc : MonoBehaviour {

    public enum TypeOfBuilds {
        Cannon,
        Fence,
        None
    }

    public delegate void UIBuildingButtonPressed(TypeOfBuilds tob);
    public static event UIBuildingButtonPressed BuildingButtonPressed;
    public delegate void UIPressedButton();
    public static event UIPressedButton UIButtonPressed;
    [SerializeField] GameObject UIWheel;
    [SerializeField] GameObject backButton;
    [SerializeField] Canvas canvas;

    [Serializable]
    public class StructuresImages {
        public List<Image> sprites = new List<Image>();
    }
    [SerializeField] List<StructuresImages> structureSprites;

    [SerializeField] Color normalPaperColor;
    [SerializeField] Color noGoldPaperColor;

    [SerializeField] List<Building> buildings;
    [SerializeField] List<float> buildingsValues;
    [SerializeField] float gold;

    private void Start() {
        BuildingCreator bc = FindObjectOfType<BuildingCreator>();
        if(bc)
            gold = bc.GetTools();

        buildingsValues = new List<float>();
        buildingsValues.Clear();
        for (int i = 0; i < buildings.Count; i++)
            buildingsValues.Add(buildings[i].GetToolsCost());
        BuildingCreator.ClickedBase += ClickedBase;
        BuildingCreator.ChangedTools += ChangedTools;
        BuildCreatorTutorial.ClickedBase += ClickedBase;
        BuildCreatorTutorial.ChangedTools += ChangedTools;
    }
    private void OnDestroy() {
        BuildingCreator.ClickedBase -= ClickedBase;
        BuildingCreator.ChangedTools -= ChangedTools;
        BuildCreatorTutorial.ChangedTools -= ChangedTools;
        BuildCreatorTutorial.ClickedBase -= ClickedBase;
    }
    void ChangedTools(float t) {
        gold = t;
    }
    void ClickedBase() {
        UIWheel.SetActive(true);
        backButton.SetActive(true);

        for (int i = 0; i < structureSprites.Count; i++)
            for (int j = 0; j < structureSprites[i].sprites.Count; j++) {
                if (gold >= buildingsValues[i])
                    structureSprites[i].sprites[j].color = normalPaperColor;
                else
                    structureSprites[i].sprites[j].color = noGoldPaperColor;
            }

        Vector3 mousePos = Input.mousePosition;

        UIWheel.transform.position = CheckScreenLimits(mousePos);
    }
    Vector3 CheckScreenLimits(Vector3 actualPos) {
        Image wheelImage = UIWheel.GetComponent<Image>();
        if (actualPos.y > Screen.height - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f)
            actualPos = new Vector3(actualPos.x, Screen.height - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f, actualPos.z);
        else if (actualPos.y < wheelImage.sprite.rect.height * canvas.scaleFactor * 0.5f)
            actualPos = new Vector3(actualPos.x, wheelImage.sprite.rect.height * canvas.scaleFactor * 0.5f, actualPos.z);

        if (actualPos.x < wheelImage.sprite.rect.width * canvas.scaleFactor * 0.5f)
            actualPos = new Vector3(wheelImage.sprite.rect.width * canvas.scaleFactor * 0.5f, actualPos.y, actualPos.z);
        else if (actualPos.x > Screen.width - (wheelImage.sprite.rect.height * canvas.scaleFactor) * 0.5f)
            actualPos = new Vector3(Screen.width - (wheelImage.sprite.rect.width * canvas.scaleFactor) * 0.5f, actualPos.y, actualPos.z);

        return actualPos;
    }
    public void ClickBackButton() {
        UIWheel.SetActive(false);
        backButton.SetActive(false);
        if (UIButtonPressed != null)
            UIButtonPressed();
    }
    public void PressButtonStructure(int button) {
        if (BuildingButtonPressed != null)
            BuildingButtonPressed((TypeOfBuilds)(button));
        if (UIButtonPressed != null)
            UIButtonPressed();
        UIWheel.SetActive(false);
        backButton.SetActive(false);
    }

}
