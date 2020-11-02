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

    public delegate void BaseSelected(Vector3 pos);
    public static event BaseSelected BSelected;

    Vector3 posSelected;
    GameObject goSelected;

    [SerializeField] GameObject tileSelected;

    void Start() {
        builds = new List<Building>();
        builds.Clear();

        enemies = new List<Enemy>();
        enemies.Clear();
        enemies.Add(null);

        cam = Camera.main;

        EnemyManager.CreatedEnemy += EnemyCreated;
        Enemy.Dead += EnemyKilled;
        Building.DestroyedBuild += DestroyedBuild;
        UIBuildings.BuildingButtonPressed += CreateStructure;
    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
        Building.DestroyedBuild -= DestroyedBuild;
        UIBuildings.BuildingButtonPressed -= CreateStructure;
    }

    void Update() {


        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200)) {
            if (hit.transform.CompareTag("Base")) {
                if (!tileSelected.activeSelf)
                    tileSelected.SetActive(true);

                tileSelected.transform.position = hit.transform.position;

                if (Input.GetMouseButtonDown(0)) {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;

                    posSelected = hit.transform.position;
                    goSelected = hit.transform.gameObject;
                    if (BSelected != null)
                        BSelected(posSelected);
                    if (ClickedBase != null)
                        ClickedBase();
                }
            }

        }
        else
            tileSelected.SetActive(false);

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

    void CreateStructure(UIBuildings.TypeOfBuilds tob) {
        buildToCreate = (int)tob;
        if (structures[buildToCreate].GetToolsCost() <= tools) {
            Building go = Instantiate(structures[buildToCreate], posSelected, structures[buildToCreate].transform.rotation, structuresParent);
            tools -= structures[buildToCreate].GetToolsCost();
            if (ChangedTools != null)
                ChangedTools(tools);
            go.SetType((Building.Type)buildToCreate);
            buildToCreate = (int)TypeOfBuilds.None;
            builds.Add(go);
            go.SetPath(paths[goSelected.GetComponent<Tile>().GetPathIndex()].pos);
            go.SetLookAt(goSelected.GetComponent<Tile>().GetLookAt());
        }
    }

    void EnemyCreated(Enemy e) {
        enemies.Add(e);
    }

    void EnemyKilled(Enemy e) {
        enemies.Remove(e);
        if (!e.GetTownAttacked())
            tools += 25;
        if (ChangedTools != null)
            ChangedTools(tools);
    }

    void DestroyedBuild(Building b) {
        builds.Remove(b);
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

    public List<Building> GetBuilds() {
        return builds;
    }
}
