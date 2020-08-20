using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] List<GameObject> spawnerPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;
    [SerializeField] int numberOfEnemiesToSpawn;
    int actualNumberOfEnemies;
    bool attacking = false;
    void Start() {
        actualNumberOfEnemies = numberOfEnemiesToSpawn;
        GameplayManager.startEnemyAttack += StartSpawning;
        GameplayManager.endEnemyAttack += StopSpawning;
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
        actualNumberOfEnemies+=3;
        attacking = false;
        StopCoroutine(PrepareEnemy());
    }

    IEnumerator PrepareEnemy() {
        yield return new WaitForSeconds(1f);
        CreateEnemies();
        StopCoroutine(PrepareEnemy());
        yield return null;
    }
    void CreateEnemies() {
        if (!attacking)
            return;

        for (int i = 0; i < actualNumberOfEnemies; i++) {
            int index = Random.Range(0, spawnerPoints.Count);
            if (spawnerPoints[index] != null) {
                GameObject go = Instantiate(enemyPrefab, spawnerPoints[index].transform.position + upset, Quaternion.identity, enemyParent);
            }
        }
        StartCoroutine(PrepareEnemy());
    }
}
