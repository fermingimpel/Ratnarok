using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject[] spawnerPoints;
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;
    bool attacking = true;

    [SerializeField] int hordes;
    [SerializeField] float timeToSpawn;
    [SerializeField] float timeToHorde;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    void Start() {
        GameplayManager.startEnemyAttack += StartSpawning;
        GameplayManager.endEnemyAttack += StopSpawning;
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartSpawning;
        GameplayManager.endEnemyAttack -= StopSpawning;
    }

    void StartSpawning() {
        attacking = true;
        StartCoroutine(CreateEnemies());
    }
    void StopSpawning() {
        attacking = false;
        StopCoroutine(PrepareEnemy());
    }

    IEnumerator PrepareEnemy() {
        yield return new WaitForSeconds(timeToHorde);
        StopCoroutine(PrepareEnemy());
        StartCoroutine(CreateEnemies());
        yield return null;
    }
    IEnumerator CreateEnemies() {
        if (!attacking)
            yield return null;

        for (int i = 0; i < hordes; i++) {
            for (int j = 0; j < spawnerPoints.Length; j++) {
                Enemy go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoints[j].transform.position, Quaternion.identity, enemyParent);
                if (CreatedEnemy != null)
                    CreatedEnemy(go);
            }
            yield return new WaitForSeconds(timeToSpawn);
        }

        StopCoroutine(CreateEnemies());
        StartCoroutine(PrepareEnemy());
        yield return null;
    }
}
