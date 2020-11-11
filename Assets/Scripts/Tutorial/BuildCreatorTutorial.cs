using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildCreatorTutorial : MonoBehaviour {
    [SerializeField] Building cannon;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform cannonParent;

    [SerializeField] float tools;

    Camera cam;

    [Serializable]
    public class Paths {
        public List<Transform> pos;
    }

    [SerializeField] List<Paths> paths;


    public delegate void ToolsChanged(float t);
    public static event ToolsChanged ChangedTools;


    public delegate void BaseClicked();
    public static event BaseClicked ClickedBase;

    Vector3 posSelected;
    GameObject goSelected;

    [SerializeField] GameObject tileSelected;
    bool canSelectTile = true;
    int actualPhase = 0;

    void Start() {
        canSelectTile = true;

        cam = Camera.main;
        UITutorial.BuildTutorialPressed += CreateTurret;
        Enemy.Dead += EnemyKilled;
        TutorialManager.TutorialPhaseChanged += ChangePhase;
        UIBuildingsDisc.UIButtonPressed += CanSelectTile;
        UITutorial.ClickedPause += CantSelectTile;
        UITutorial.ClickedResume += CanSelectTile;
    }
    private void OnDisable() {
        UITutorial.BuildTutorialPressed -= CreateTurret;
        Enemy.Dead -= EnemyKilled;
        TutorialManager.TutorialPhaseChanged -= ChangePhase;
        UIBuildingsDisc.UIButtonPressed -= CanSelectTile;
        UITutorial.ClickedPause -= CantSelectTile;
        UITutorial.ClickedResume -= CanSelectTile;
    }

    void Update() {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (hit.transform.CompareTag("Base")) {
                if (!tileSelected.activeSelf && canSelectTile)
                    tileSelected.SetActive(true);
                else if (!canSelectTile)
                    tileSelected.SetActive(false);

                tileSelected.transform.position = hit.transform.position;
                if (Input.GetMouseButtonDown(0)) {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    canSelectTile = false;
                    tileSelected.SetActive(false);
                    posSelected = hit.transform.position;
                    goSelected = hit.transform.gameObject;
                    if (ClickedBase != null)
                        ClickedBase();
                }
            }
        }
        else {
            if (tileSelected.activeSelf)
                tileSelected.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1)) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (Physics.Raycast(ray, out hit, 200)) {
                if (hit.transform.CompareTag("Structure")) {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }



    void ChangePhase(int p) {
        actualPhase = p;
    }

    void CreateTurret() {
        if (cannon.GetToolsCost() <= tools) {
            Building go = Instantiate(cannon, posSelected, cannon.transform.rotation, cannonParent);
            tools -= cannon.GetToolsCost();
            if (ChangedTools != null)
                ChangedTools(tools);
            go.SetPath(paths[goSelected.GetComponent<Tile>().GetPathIndex()].pos);
            go.SetLookAt(goSelected.GetComponent<Tile>().GetLookAt());
        }
        CanSelectTile();
    }
    void CantSelectTile() {
        canSelectTile = false;
    }
    void CanSelectTile() {
        canSelectTile = true;
    }

    void EnemyKilled(Enemy e) {
        tools += 25;
        if (ChangedTools != null)
            ChangedTools(tools);
    }
}
