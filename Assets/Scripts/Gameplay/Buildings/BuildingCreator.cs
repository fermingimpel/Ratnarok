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

    [SerializeField] float gold;

    Camera cam;

    [SerializeField] List<Building> builds;
    [SerializeField] List<Enemy> enemies;

    [Serializable]
    public class Paths {
        public List<Transform> pos;
    }

    [SerializeField] List<Paths> paths;

    public delegate void GoldChanged(float g);
    public static event  GoldChanged ChangedGold;

    public delegate void BaseClicked();
    public static event BaseClicked ClickedBase;

    public delegate void BaseSelected(Vector3 pos);
    public static event BaseSelected BSelected;

    Vector3 posSelected;
    GameObject goSelected;

    [SerializeField] GameObject tileSelected;
    bool canSelectTile = false;

    [SerializeField] float goldPerKill;
    [SerializeField] bool defending = false;
    private void Awake() {
        EnemyManager.CreatedEnemy += EnemyCreated;
        Enemy.Dead += EnemyKilled;
        Building.DestroyedBuild += DestroyedBuild;
        UIBuildingsDisc.BuildingButtonPressed += CreateStructure;
        UIBuildingsDisc.UIButtonPressed += CanSelectTile;
        UIGameplay.ClickedPause += CantSelectTile;
        UIGameplay.ClickedResume += CanSelectTile;
        GameplayManager.StartEnemyAttack += StartDefend;
        GameplayManager.EndEnemyAttack += StopDefend;
    }
    void Start() {
        builds = new List<Building>();
        builds.Clear();

        enemies = new List<Enemy>();
        enemies.Clear();
        enemies.Add(null);
        canSelectTile = true;
        cam = Camera.main;

        if (ChangedGold != null)
            ChangedGold(gold);
        defending = false;
    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
        Building.DestroyedBuild -= DestroyedBuild;
        UIBuildingsDisc.BuildingButtonPressed -= CreateStructure;
        UIBuildingsDisc.UIButtonPressed -= CanSelectTile;
        UIGameplay.ClickedPause -= CantSelectTile;
        UIGameplay.ClickedResume -= CanSelectTile;
        GameplayManager.StartEnemyAttack -= StartDefend;
        GameplayManager.EndEnemyAttack -= StopDefend;
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
                    Building b = hit.transform.GetComponent<Building>();
                    if (b)
                        DeleteStructure(b);
                }
            }
        }
    }

    void CreateStructure(int tob) {
        buildToCreate = tob;
        if (structures[buildToCreate].GetToolsCost() <= gold) {
            Building go = Instantiate(structures[buildToCreate], posSelected, structures[buildToCreate].transform.rotation, structuresParent);
            gold -= structures[buildToCreate].GetToolsCost();
            if (ChangedGold != null)
                ChangedGold(gold);
            go.SetType((Building.Type)buildToCreate);
            buildToCreate = (int)TypeOfBuilds.None;
            builds.Add(go);
            go.SetPath(paths[goSelected.GetComponent<Tile>().GetPathIndex()].pos);
            go.SetLookAt(goSelected.GetComponent<Tile>().GetLookAt());
            go.SetDefending(defending);
        }
        CanSelectTile();
    }
    void DeleteStructure(Building b) {
        gold += (b.GetHP() / b.GetMaxHP()) * b.GetToolsCost();
        if (ChangedGold != null)
            ChangedGold(gold);
        Destroy(b.transform.gameObject);
    }
    void CantSelectTile() {
        canSelectTile = false;
        tileSelected.SetActive(false);
    }
    void CanSelectTile() {
        canSelectTile = true;
    }
    void StartDefend() {
        defending = true;
    }
    void StopDefend() {
        defending = false;
    }
    void EnemyCreated(Enemy e) {
        enemies.Add(e);
    }

    void EnemyKilled(Enemy e) {
        enemies.Remove(e);
        if (!e.GetTownAttacked())
            gold += goldPerKill;
        if (ChangedGold != null)
            ChangedGold(gold);
    }

    void DestroyedBuild(Building b) {
        builds.Remove(b);
    }

    public void UseGold(int g) {
        gold -= g;
        if (ChangedGold != null)
            ChangedGold(gold);
    }
    public float GetGold() {
        return gold;
    }
    public void AddGold(int g) {
        gold += g;
        if (ChangedGold != null)
            ChangedGold(gold);
    }

    public List<Building> GetBuilds() {
        return builds;
    }
}
