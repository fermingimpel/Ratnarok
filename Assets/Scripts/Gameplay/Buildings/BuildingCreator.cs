using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] GameObject buildingsUI;
    [SerializeField] GameObject buildingInformationUI;
    [SerializeField] Build[] structures;
    [SerializeField] UnitAlly[] unitsPrefab;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform towerParent;

    Vector3 posSelected;

    Vector3 offsetButtonsBuildUI = new Vector3(100, 0, 0);

    int buildToCreate = 1;

    public enum TypeOfBuilds {
        None,
        Tower,
        KnivesSpinner
    }

    Camera cam;

    [SerializeField] int cheese;
    [SerializeField] int cheesePerKill;

    [SerializeField] List<Build> builds;
    [SerializeField] List<UnitAlly> units;
    [SerializeField] List<Enemy> enemies;

    public delegate void CheeseChanged(int c);
    public static event CheeseChanged ChangedCheese;

    public delegate void BuildsChanged(List<Build> b);
    public static event BuildsChanged ChangedBuilds;

    public delegate void ClickedBuild(Build b, Vector3 mp);
    public static event ClickedBuild BuildClicked;

    void Start() {
        buildingsUI.SetActive(false);

        if (ChangedCheese != null)
            ChangedCheese(cheese);

        builds = new List<Build>();
        builds.Clear();

        enemies = new List<Enemy>();
        enemies.Clear();
        enemies.Add(null);

        cam = Camera.main;

        EnemyManager.CreatedEnemy += EnemyCreated;
        Enemy.Dead += EnemyKilled;
        UIBuildings.BuildingButtonPressed += SelectTypeOfStructure;
        Build.DestroyedBuild += BuildDestroyed;
    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
        UIBuildings.BuildingButtonPressed -= SelectTypeOfStructure;
        Build.DestroyedBuild -= BuildDestroyed;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (buildingsUI.activeSelf)
                buildingsUI.SetActive(false);
            if (buildingInformationUI.activeSelf)
                buildingInformationUI.SetActive(false);
        }

        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (Input.GetMouseButtonDown(0)) {

                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (hit.transform.CompareTag("Base")) {
                    posSelected = new Vector3((int)hit.transform.position.x, hit.point.y + upset.y, (int)hit.transform.position.z);
                    if (!buildingsUI.activeSelf)
                        buildingsUI.SetActive(true);
                    if (buildingInformationUI.activeSelf)
                        buildingInformationUI.SetActive(false);
                    buildingsUI.transform.position = mousePos + offsetButtonsBuildUI;
                }
                else if (hit.transform.CompareTag("Build")) {
                    if (BuildClicked != null) {
                       if (!buildingInformationUI.activeSelf)
                           buildingInformationUI.SetActive(true);
                        Build b = hit.transform.GetComponent<Build>();
                        if (b != null)
                            BuildClicked(b, mousePos);
                    }
                }
                //else if (hit.transform.CompareTag("Road")) {
                //    //Meter menu de spawn de unidades aliadas
                //}
            }
        }

    }

    void BuildDestroyed(Build b) {
        builds.Remove(b);
        if (ChangedBuilds != null)
            ChangedBuilds(builds);
    }

    void SelectTypeOfStructure(UIBuildings.TypeOfBuilds tob) {
        buildToCreate = (int)tob;
        if (structures[buildToCreate] != null) {
            if (structures[buildToCreate].GetCheeseCost() <= cheese) {
                Build go = Instantiate(structures[buildToCreate], posSelected, Quaternion.identity, towerParent);
                builds.Add(go);
                go.SetEnemyList(enemies);
                go.SetBuildCreator(this);
                cheese -= structures[buildToCreate].GetCheeseCost();
                if (ChangedCheese != null)
                    ChangedCheese(cheese);

                if (ChangedBuilds != null)
                    ChangedBuilds(builds);
            }
        }

        buildingsUI.SetActive(false);
    }

    void EnemyCreated(Enemy e) {
        enemies.Add(e);
        for (int i = 0; i < builds.Count; i++)
            if (builds[i] != null)
                builds[i].SetEnemyList(enemies);
   
        for (int i = 0; i < units.Count; i++)
            if (units[i] != null)
                units[i].SetEnemyList(enemies);
    }

    void EnemyKilled(Enemy e) {
        cheese += cheesePerKill;

        if (ChangedCheese != null)
            ChangedCheese(cheese);

        enemies.Remove(e);
        for (int i = 0; i < builds.Count; i++)
            if (builds[i] != null)
                builds[i].SetEnemyList(enemies);

        for (int i = 0; i < units.Count; i++)
            if (units[i] != null)
                units[i].SetEnemyList(enemies);
    }
    public void UseCheese(int c) {
        cheese -= c;
        if (ChangedCheese != null)
            ChangedCheese(cheese);
    }
    public int GetCheese() {
        return cheese;
    }
    public void AddCheese(int c) {
        cheese += c;
        if (ChangedCheese != null)
            ChangedCheese(cheese);
    }
}
