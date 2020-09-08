using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] GameObject buildingsUI;
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
        GameplayManager.endEnemyAttack += StartDefendPhase;
    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
        UIBuildings.BuildingButtonPressed -= SelectTypeOfStructure;
        Build.DestroyedBuild -= BuildDestroyed;
        GameplayManager.endEnemyAttack -= StartDefendPhase;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && buildingsUI.activeSelf)
            buildingsUI.SetActive(false);

        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (Input.GetMouseButtonDown(0)) {
                if (hit.transform.CompareTag("Base")) {
                    //if (structures[buildToCreate].GetGoldCost() <= gold) {
                    //    Vector3 pos = new Vector3((int)hit.transform.position.x, hit.point.y + upset.y, (int)hit.transform.position.z);
                    //    Build go = Instantiate(structures[buildToCreate], pos, Quaternion.identity, towerParent);
                    //    builds.Add(go);
                    //    go.SetEnemyList(enemies);
                    //    gold -= structures[buildToCreate].GetGoldCost();
                    //    if (ChangedGold != null)
                    //        ChangedGold(gold);
                    //
                    //    if (ChangedBuilds != null)
                    //        ChangedBuilds(builds);
                    //}
                    posSelected = new Vector3((int)hit.transform.position.x, hit.point.y + upset.y, (int)hit.transform.position.z);
                    buildingsUI.SetActive(true);
                    buildingsUI.transform.position = mousePos + offsetButtonsBuildUI;
                }
            }
        }

    }

    void StartDefendPhase() {
        enemies.Clear();
        enemies.Add(null);
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
}
