using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStructureDisc : MonoBehaviour {
    [SerializeField] StructureCreator structureCreator;
    [SerializeField] GameObject UIWheel;
    [SerializeField] GameObject backButton;

    enum TypeOfSprite {
        Ready,
        NotReady
    }

    [Serializable]
    public class StructuresImages {
        public List<Sprite> sprites = new List<Sprite>();
    }
    
    [SerializeField] List<StructuresImages> structuresSprites;
    [SerializeField] List<Image> structuresInPapers;
    [SerializeField] List<Image> discPapers;
    [SerializeField] List<TextMeshProUGUI> cheeseCostTexts;

    [SerializeField] Color normalPaperColor;
    [SerializeField] Color noCheesePaperColor;
    [SerializeField] Canvas canvas;

    public static Action CreatedStructure;

    public void ActivateStructuresWheel() {
        UIWheel.SetActive(true);
        backButton.SetActive(true);

        Vector3 mousePos = Input.mousePosition;
        UIWheel.transform.position = CheckScreenLimits(mousePos);

        for (int i = 0; i < cheeseCostTexts.Count; i++)
            if (cheeseCostTexts[i] != null)
                cheeseCostTexts[i].text = structureCreator.GetCheeseCost((StructureCreator.TypeOfStructure)i).ToString();
    
    }
    public void DesactivateStructuresWheel() {
        UIWheel.SetActive(false);
        backButton.SetActive(false);
    }
    public void UpdateWheel(float ch) {
        for (int i = 0; i < structuresSprites.Count; i++) {
            if (ch >= structureCreator.GetCheeseCost((StructureCreator.TypeOfStructure)i) && structureCreator.GetCanCreateStructure((StructureCreator.TypeOfStructure)i)) {
                if (discPapers[i] != null && structuresInPapers[i] != null) {
                    discPapers[i].color = normalPaperColor;
                    structuresInPapers[i].sprite = structuresSprites[i].sprites[(int)TypeOfSprite.Ready];
                }
            }
            else {
                if (discPapers[i] != null && structuresInPapers[i] != null) {
                    discPapers[i].color = noCheesePaperColor;
                    structuresInPapers[i].sprite = structuresSprites[i].sprites[(int)TypeOfSprite.NotReady];
                }
            }
        }
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
    public void ClickStructure(StructureCreator.TypeOfStructure tos, ref float ch, Vector3 pos, Tile t) {
        if (ch >= structureCreator.GetCheeseCost(tos) && structureCreator.GetCanCreateStructure(tos)) {
            structureCreator.CreateStructure(tos, pos, t);
            ch -= structureCreator.GetCheeseCost(tos);
            if (CreatedStructure != null)
                CreatedStructure();
        }
    }
}
