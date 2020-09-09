using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] GameObject buildingsUI;
    [SerializeField] GameObject buildingInformationUI;
    [SerializeField] Build[] structures;
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

    [SerializeField] int gold;
    [SerializeField] int goldPerKill;

    [SerializeField] List<Build> builds;
    [SerializeField] List<Enemy> enemies;

    public delegate void GoldChanged(int gold);
    public static event GoldChanged ChangedGold;

    public delegate void BuildsChanged(List<Build> b);
    public static event BuildsChanged ChangedBuilds;

    public delegate void ClickedBuild(Build b, Vector3 mp);
    public static event ClickedBuild BuildClicked;

    void Start() {
        buildingsUI.SetActive(false);

        if (ChangedGold != null)
            ChangedGold(gold);

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
            if (structures[buildToCreate].GetGoldCost() <= gold) {
                Build go = Instantiate(structures[buildToCreate], posSelected, Quaternion.identity, towerParent);
                builds.Add(go);
                go.SetEnemyList(enemies);
                go.SetBuildCreator(this);
                gold -= structures[buildToCreate].GetGoldCost();
                if (ChangedGold != null)
                    ChangedGold(gold);

                if (ChangedBuilds != null)
                    ChangedBuilds(builds);
            }
        }

        buildingsUI.SetActive(false);
    }

    void EnemyCreated(Enemy e) {
        enemies.Add(e);
        for (int i = 0; i < builds.Count; i++)
            builds[i].SetEnemyList(enemies);
    }

    void EnemyKilled(Enemy e) {
        gold += goldPerKill;

        if (ChangedGold != null)
            ChangedGold(gold);

        enemies.Remove(e);
        for (int i = 0; i < builds.Count; i++)
            builds[i].SetEnemyList(enemies);
    }
    public void UseGold(int g) {
        gold -= g;
        if (ChangedGold != null) 
            ChangedGold(gold);
    }
    public int GetGold() {
        return gold;
    }
}
