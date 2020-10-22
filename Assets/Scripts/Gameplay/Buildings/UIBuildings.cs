using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildings : MonoBehaviour {

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
            if (UIButtonPressed != null)
                UIButtonPressed();
        }
    }

    void ClickedBase() {
        UIWheel.SetActive(true);
        backButton.SetActive(true);

        Vector3 mousePos = Input.mousePosition;
        if (mousePos.y > Screen.height - (UIWheel.GetComponent<Image>().sprite.rect.height * canvas.scaleFactor) * 0.5f) 
            mousePos = new Vector3(mousePos.x, Screen.height - (UIWheel.GetComponent<Image>().sprite.rect.height * canvas.scaleFactor) * 0.5f, mousePos.z);
        else if (mousePos.y < UIWheel.GetComponent<Image>().sprite.rect.height * canvas.scaleFactor * 0.5f) 
            mousePos = new Vector3(mousePos.x, UIWheel.GetComponent<Image>().sprite.rect.height * canvas.scaleFactor * 0.5f, mousePos.z);
       
        if (mousePos.x < UIWheel.GetComponent<Image>().sprite.rect.width * canvas.scaleFactor * 0.5f) 
            mousePos = new Vector3(UIWheel.GetComponent<Image>().sprite.rect.width * canvas.scaleFactor * 0.5f, mousePos.y, mousePos.z);
        else if (mousePos.x > Screen.width - (UIWheel.GetComponent<Image>().sprite.rect.height * canvas.scaleFactor) * 0.5f) 
            mousePos = new Vector3(Screen.width - (UIWheel.GetComponent<Image>().sprite.rect.width * canvas.scaleFactor) * 0.5f, mousePos.y, mousePos.z);
        
        UIWheel.transform.position = mousePos;
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
