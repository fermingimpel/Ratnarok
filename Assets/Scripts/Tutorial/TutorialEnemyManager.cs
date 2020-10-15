using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyManager : MonoBehaviour {
    [SerializeField] GameObject spawnerPoint;

    [SerializeField] List<Transform> path;

    [SerializeField] Enemy enemy;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;

    int enemiesCreated = 0;

    [SerializeField] Town town;
    int phase = 0;
    private void Start() {
        TutorialManager.TutorialPhaseChanged += ChangedPhase;
    }
    private void OnDisable() {
        TutorialManager.TutorialPhaseChanged -= ChangedPhase;
    }

    void ChangedPhase(int p) {
        phase = p;
        if (p == 5) {
            StartCoroutine(PrepareEnemyFirstPath());
        }
    }

    void SpawnEnemyFirstPath() {
        Enemy go = Instantiate(enemy, spawnerPoint.transform.position + upset, enemy.transform.rotation, enemyParent);
        go.SetPath(path);
        go.SetTown(town);
        enemiesCreated++;
        if (phase == 5 && enemiesCreated < 5)
            StartCoroutine(PrepareEnemyFirstPath());
    }

    IEnumerator PrepareEnemyFirstPath() {
        yield return new WaitForSeconds(2.0f);
        SpawnEnemyFirstPath();
        yield return null;
    }
}
