using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] Building[] structures;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform structuresParent;


    public enum TypeOfBuilds {
        Cannon,
        ToolsGenerator,
        Fence,
        Catapult,
        Flamethrower,
        Crossbow,
        None
    }

    int buildToCreate = (int)TypeOfBuilds.None;

    [SerializeField] int tools = 50;

    Camera cam;

    [SerializeField] List<Building> builds;
    [SerializeField] List<Enemy> enemies;

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

    void Start() {
        builds = new List<Building>();
        builds.Clear();

        enemies = new List<Enemy>();
        enemies.Clear();
        enemies.Add(null);

        cam = Camera.main;

        EnemyManager.CreatedEnemy += EnemyCreated;
        Enemy.Dead += EnemyKilled;
        UIBuildings.BuildingButtonPressed += CreateStructure;
    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
        UIBuildings.BuildingButtonPressed -= CreateStructure;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 200)) {
                if (hit.transform.CompareTag("Base")) {
                    posSelected = hit.transform.position;
                    goSelected = hit.transform.gameObject;
                    if (ClickedBase != null)
                        ClickedBase();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200)) {
                if (hit.transform.CompareTag("Structure")) {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void CreateStructure(UIBuildings.TypeOfBuilds tob) {
        buildToCreate = (int)tob;
        if (structures[buildToCreate].GetToolsCost() <= tools) {
            Building go = Instantiate(structures[buildToCreate], posSelected, structures[buildToCreate].transform.rotation, structuresParent);
            tools -= structures[buildToCreate].GetToolsCost();
            if (ChangedTools != null)
                ChangedTools(tools);
            buildToCreate = (int)TypeOfBuilds.None;
            go.SetPath(paths[goSelected.GetComponent<Tile>().GetPathIndex()].pos);
            go.SetLookAt(goSelected.GetComponent<Tile>().GetLookAt());
        }
    }

    void EnemyCreated(Enemy e) {
        enemies.Add(e);
    }

    void EnemyKilled(Enemy e) {
        enemies.Remove(e);
        tools += 25;
        if (ChangedTools != null)
            ChangedTools(tools);
    }

    public void UseTools(int t) {
        tools -= t;
        if (ChangedTools != null)
            ChangedTools(tools);
    }
    public int GetTools() {
        return tools;
    }
    public void AddTools(int t) {
        tools += t;
        if (ChangedTools != null)
            ChangedTools(tools);
    }
}
