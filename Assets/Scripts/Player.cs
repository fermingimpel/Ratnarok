using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] GameObject tower;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform towerParent;
    [SerializeField] int limitTowers;
    int actualTowers;
    bool canCreateTowers = true;
    Camera cam;

    public delegate void TowerUsed();
    public static event TowerUsed TowerCreated;

    void Start() {
        cam = Camera.main;
        canCreateTowers = true;
        actualTowers = limitTowers;
        GameplayManager.endEnemyAttack += StartCreating;
        GameplayManager.startEnemyAttack += StopCreating;
    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= StartCreating;
        GameplayManager.startEnemyAttack -= StopCreating;
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
        if(Physics.Raycast(ray,out hit, 200)) {
            if (Input.GetMouseButtonDown(0)) {
                if(hit.transform.tag == "Base") {
                    GameObject go = Instantiate(tower, new Vector3(hit.point.x, 1.5f, hit.point.z), Quaternion.identity, towerParent);
                    actualTowers--;
                    if (TowerCreated != null)
                        TowerCreated();
                }
            }
        }
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
        if (TowerCreated != null)
            TowerCreated();
    }

}
