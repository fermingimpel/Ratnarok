﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCreator : MonoBehaviour {
    [SerializeField] Build[] structures;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform structuresParent;


    public enum TypeOfBuilds {
        Cannon,
        ToolsGenerator,
        Fence,
        Catapult,
        Flamethrower,
        None
    }

    int buildToCreate = (int)TypeOfBuilds.None;

    [SerializeField] int tools = 50;

    Camera cam;

    [SerializeField] List<Build> builds;
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
        builds = new List<Build>();
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
        //if (Input.anyKeyDown) {
        //    switch (Input.inputString) {
        //        case "1":
        //            buildToCreate = (int)TypeOfBuilds.Cannon;
        //            break;
        //        case "2":
        //            buildToCreate = (int)TypeOfBuilds.ToolsGenerator;
        //            break;
        //        case "3":
        //            buildToCreate = (int)TypeOfBuilds.Fence;
        //            break;
        //        case "4":
        //            buildToCreate = (int)TypeOfBuilds.Catapult;
        //            break;
        //        case "5":
        //            buildToCreate = (int)TypeOfBuilds.Flamethrower;
        //            break;
        //    }
        //}
        //
        //if (Input.GetMouseButtonDown(0) && buildToCreate != (int)TypeOfBuilds.None) {
        //    Vector3 mousePos = Input.mousePosition;
        //    Ray ray = cam.ScreenPointToRay(mousePos);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 200)) {
        //        if (hit.transform.CompareTag("Base")) {
        //            if (structures[buildToCreate] != null)
        //                if (structures[buildToCreate].GetToolsCost() <= tools) {
        //                    Build go = Instantiate(structures[buildToCreate], hit.transform.position, structures[buildToCreate].transform.rotation, structuresParent);
        //                    tools -= structures[buildToCreate].GetToolsCost();
        //                    if (ChangedTools != null)
        //                        ChangedTools(tools);
        //                    buildToCreate = (int)TypeOfBuilds.None;
        //                    go.SetPath( paths[hit.transform.gameObject.GetComponent<Tile>().GetPathIndex()].pos);
        //                    go.SetLookAt(hit.transform.gameObject.GetComponent<Tile>().GetLookAt());
        //                }
        //        }
        //    }
        //}

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
    }

    void CreateStructure(UIBuildings.TypeOfBuilds tob) {
        buildToCreate = (int)tob;
        if (structures[buildToCreate].GetToolsCost() <= tools) {
            Build go = Instantiate(structures[buildToCreate], posSelected, structures[buildToCreate].transform.rotation, structuresParent);
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
