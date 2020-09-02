using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] Build[] structures;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform towerParent;

    int buildToCreate = 0;
    
    public enum TypeOfBuilds {
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

    void Start() {

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

    }

    private void OnDisable() {
        EnemyManager.CreatedEnemy -= EnemyCreated;
        Enemy.Dead -= EnemyKilled;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            buildToCreate = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            buildToCreate = 1;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (Input.GetMouseButtonDown(0)) {
                if (hit.transform.tag == "Base") {
                    if (structures[buildToCreate].GetGoldCost() <= gold) {
                        Vector3 pos = new Vector3((int)hit.transform.position.x, hit.point.y + upset.y, (int)hit.transform.position.z);
                        Build go = Instantiate(structures[buildToCreate], pos, Quaternion.identity, towerParent);
                        builds.Add(go);
                        go.SetEnemyList(enemies);
                        gold -= structures[buildToCreate].GetGoldCost();
                        if (ChangedGold != null)
                            ChangedGold(gold);

                        //if (TowerCreated != null)
                        //    TowerCreated();
                    }
                }
            }
        }
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
