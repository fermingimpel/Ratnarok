using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] GameObject tower;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform towerParent;
    [SerializeField] int limitTowers;
    int actualTowers;
    bool canCreateTowers = true;
    Camera cam;

   //public delegate void TowerUsed();
   //public static event TowerUsed TowerCreated;

    [SerializeField] List<Tower> towers;
    [SerializeField] List<GameObject> enemies;

    void Start() {
        towers = new List<Tower>();
        towers.Clear();

        enemies = new List<GameObject>();
        enemies.Clear();
        enemies.Add(null);

        cam = Camera.main;
        canCreateTowers = true;
        actualTowers = limitTowers;
        GameplayManager.endEnemyAttack += StartCreating;
        GameplayManager.startEnemyAttack += StopCreating;

        EnemyManager.CreatedEnemy += AddEnemyToList;
        Enemy.Dead += RemoveEnemyInList;

    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= StartCreating;
        GameplayManager.startEnemyAttack -= StopCreating;

        EnemyManager.CreatedEnemy -= AddEnemyToList;
        Enemy.Dead -= RemoveEnemyInList;
    }

    // Update is called once per frame
    void Update() {
        if (!canCreateTowers)
            return;

        if (actualTowers <= 0)
            return;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (Input.GetMouseButtonDown(0)) {
                if (hit.transform.tag == "Base") {
                    Vector3 pos = new Vector3((int)hit.transform.position.x, hit.point.y, (int)hit.transform.position.z);
                    pos += upset;
                    GameObject go = Instantiate(tower, pos, Quaternion.identity, towerParent);
                    Tower t = go.GetComponent<Tower>();
                   
                    towers.Add(t);
                    t.SetEnemyList(enemies);
                    actualTowers--;
                   //if (TowerCreated != null)
                   //    TowerCreated();

                }
            }
        }
    }

    void AddEnemyToList(GameObject e) {
        enemies.Add(e);
        for (int i = 0; i < towers.Count; i++)
            towers[i].SetEnemyList(enemies);
    }

    void RemoveEnemyInList(GameObject e) {
        enemies.Remove(e);
        for (int i = 0; i < towers.Count; i++)
            towers[i].SetEnemyList(enemies);
    }

    public int GetActualTowers() {
        return actualTowers;
    }
    void StopCreating() {
        canCreateTowers = false;
    }
    void StartCreating() {
        canCreateTowers = true;
        actualTowers = limitTowers;
        //if (TowerCreated != null)
        //    TowerCreated();
    }
}
