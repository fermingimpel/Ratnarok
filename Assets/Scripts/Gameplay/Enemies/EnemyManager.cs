using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject spawnerPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;
    bool attacking = true;
    void Start() {
        GameplayManager.startEnemyAttack += StartSpawning;
        GameplayManager.endEnemyAttack += StopSpawning;
        StartCoroutine(PrepareEnemy());
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartSpawning;
        GameplayManager.endEnemyAttack -= StopSpawning;
    }

    void StartSpawning() {
        attacking = true;
        StartCoroutine(PrepareEnemy());
    }
    void StopSpawning() {
        attacking = false;
        StopCoroutine(PrepareEnemy());
    }

    IEnumerator PrepareEnemy() {
        yield return new WaitForSeconds(2f);
        CreateEnemies();
        StopCoroutine(PrepareEnemy());
        yield return null;
    }
    void CreateEnemies() {
        if (!attacking)
            return;

        GameObject go = Instantiate(enemyPrefab, spawnerPoints.transform.position, Quaternion.identity, enemyParent);
        StartCoroutine(PrepareEnemy());
    }
}
