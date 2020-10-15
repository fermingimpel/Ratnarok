using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildCreatorTutorial : MonoBehaviour {
    [SerializeField] Building cannon;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform cannonParent;

    [SerializeField] int tools = 200;

    Camera cam;

    [Serializable]
    public class Paths {
        public List<Transform> pos;
    }

    [SerializeField] List<Paths> paths;


    public delegate void ToolsChanged(int t);
    public static event ToolsChanged ChangedTools;


    public delegate void BaseClicked();
    public static event BaseClicked ClickedBase;

    Vector3 posSelected;
    GameObject goSelected;

    int actualPhase = 0;

    void Start() {
        cam = Camera.main;
        UITutorial.BuildTutorialPressed += CreateTurret;
        Enemy.Dead += EnemyKilled;
        TutorialManager.TutorialPhaseChanged += ChangePhase;
    }
    private void OnDisable() {
        UITutorial.BuildTutorialPressed -= CreateTurret;
        Enemy.Dead -= EnemyKilled;
        TutorialManager.TutorialPhaseChanged -= ChangePhase;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200)) {
                if (hit.transform.CompareTag("Base")) {
                    posSelected = hit.transform.position;
                    goSelected = hit.transform.gameObject;
                    if (ClickedBase != null)
                        ClickedBase();
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
    }
    void EnemyKilled(Enemy e) {
        tools += 25;
        if (ChangedTools != null)
            ChangedTools(tools);
    }
}
