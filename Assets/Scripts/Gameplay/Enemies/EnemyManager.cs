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
        Debug.Log("Entra start spawning");
        attacking = true;
        StartCoroutine(CreateEnemies());
    }
    void StopSpawning() {
        Debug.Log("Entra stop spawning");
        attacking = false;
        StopCoroutine(PrepareEnemy());
        StopCoroutine(CreateEnemies());
    }

    IEnumerator PrepareEnemy() {
        yield return new WaitForSeconds(timeToHorde);
        if (attacking) {
            StopCoroutine(PrepareEnemy());
            StartCoroutine(CreateEnemies());
        }
        else
            StopCoroutine(PrepareEnemy());
        yield return null;
    }
    IEnumerator CreateEnemies() {
        if (!attacking) {
            StopCoroutine(CreateEnemies());
            yield return null;
        }
        for (int i = 0; i < hordes; i++) {
            for (int j = 0; j < spawnerPoints.Length; j++) {
                Enemy go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoints[j].transform.position, Quaternion.identity, enemyParent);
                if (CreatedEnemy != null)
                    CreatedEnemy(go);
            }
            yield return new WaitForSeconds(timeToSpawn);
        }

        if (attacking) {
            StopCoroutine(CreateEnemies());
            StartCoroutine(PrepareEnemy());
        }
        else {
            StopCoroutine(PrepareEnemy());
            StopCoroutine(CreateEnemies());
        }
        yield return null;
    }
}
