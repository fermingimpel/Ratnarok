using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] StructureCreator structureCreator;
    [SerializeField] UIStructureDisc structureDisc;

    [SerializeField] float cheese = 100;
    public delegate void CheeseChanged(float c);
    public static event CheeseChanged ChangedCheese;

    Camera cam;
    readonly int LeftClickButtonNumber = 0;
    readonly int RightClickButtonNumber = 1;

    [SerializeField] CameraController cameraController;

    float timerToUpdateSprites = 0;
    const float maxTimerToUpdateSprites = 0.15f;

    GameObject tileClicked;
    [SerializeField] GameObject goCursorOverTile;
    bool canSelectTile = true;

    [SerializeField] float timeInPreparationPhase;

    [SerializeField] EnemyManager enemyManager;

    [SerializeField] UIGameplay uiGameplay;

    public float timeScale;

    bool gamePaused = false;
    private void Start() {
        cam = Camera.main;
        Town.TownDestroyed += PlayerLose;
        EnemyManager.AllEnemiesEliminated += PlayerWin;
        UIStructureDisc.CreatedStructure += StructureCreated;
        UIGameplay.ClickedPause += PauseGame;
        UIGameplay.ClickedResume += ResumeGame;
        uiGameplay.ChangeCheese(cheese);
        StartCoroutine(PreparationPhase());
    }
    private void OnDisable() {
        Town.TownDestroyed -= PlayerLose;
        EnemyManager.AllEnemiesEliminated -= PlayerWin;
        UIStructureDisc.CreatedStructure -= StructureCreated;
        UIGameplay.ClickedPause  -= PauseGame;
        UIGameplay.ClickedResume -= ResumeGame;
    }
    private void Update() {
        if (gamePaused)
            return;

        Time.timeScale = timeScale;
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200)) {
            if (hit.transform.CompareTag("Base")) {
                if (canSelectTile)
                    goCursorOverTile.SetActive(true);
                else
                    goCursorOverTile.SetActive(false);

                goCursorOverTile.transform.position = new Vector3(hit.transform.position.x, 0.11f, hit.transform.position.z);

                if (Input.GetMouseButtonDown(LeftClickButtonNumber)) {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    canSelectTile = false;
                    tileClicked = hit.transform.gameObject;
                    AkSoundEngine.PostEvent("click_torret_menu", this.gameObject);
                    cameraController.LockCameraMovement();
                    structureDisc.ActivateStructuresWheel();
                }
            }
        }
        else
            goCursorOverTile.SetActive(false);

        if (Input.GetMouseButtonDown(RightClickButtonNumber)) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if(Physics.Raycast(ray, out hit, 200)) {
                if (hit.transform.CompareTag("Structure")) {
                    Structure s = hit.transform.GetComponent<Structure>();
                    if (s) {
                        cheese += (s.GetHP() / s.GetMaxHP()) * s.GetCheeseCost();
                        if (ChangedCheese != null)
                            ChangedCheese(cheese);
                        s.HitStructure(s.GetMaxHP());
                        uiGameplay.ChangeCheese(cheese);
                    }

                }
            }

        }

        timerToUpdateSprites += Time.deltaTime;
        if (timerToUpdateSprites >= maxTimerToUpdateSprites) {
            timerToUpdateSprites = 0.0f;
            structureDisc.UpdateWheel(cheese);
        }
    }

    void PauseGame() {
        gamePaused = true;
        structureCreator.StopDefending();
        enemyManager.PauseAttack();
    }
    void ResumeGame() {
        gamePaused = false;
        structureCreator.StartDefending();
        enemyManager.ResumeAttack();
    }

    public void ClickStructureButton(int tos) {
        structureDisc.ClickStructure((StructureCreator.TypeOfStructure)tos, ref cheese, tileClicked.transform.position, tileClicked.GetComponent<Tile>());
        cameraController.UnlockCameraMovement();
        canSelectTile = true;
    }
    public void ClickBackButton() {
        cameraController.UnlockCameraMovement();
        structureDisc.DesactivateStructuresWheel();
        canSelectTile = true;
    }

    IEnumerator PreparationPhase() {
        float preAttackTime = 2.0f;
        uiGameplay.PreStartGameUI();
        yield return new WaitForSeconds(timeInPreparationPhase - preAttackTime);
        AkSoundEngine.PostEvent("level_prep", this.gameObject);
        yield return new WaitForSeconds(preAttackTime);
        enemyManager.StartAttack();
        structureCreator.StartDefending();
        uiGameplay.StartGameUI();
    }

    void PlayerLose() {
        StopThings();
        uiGameplay.ActivateLoseScreen();
    }
    void PlayerWin() {
        StopThings();
        uiGameplay.ActivateWinScreen();
    }
    void StopThings() {
        enemyManager.StopAttack();
        structureCreator.StopDefending();
        cameraController.LockCameraMovement();
    }
    void StructureCreated() {
        structureDisc.DesactivateStructuresWheel();
        uiGameplay.ChangeCheese(cheese);
    }
}
