using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject[] spawnerPoints;
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;

    [SerializeField] int enemiesToCreate;
    [SerializeField] float timeToSpawn;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    void Start() {
        GameplayManager.StartEnemyAttack += StartAttack;
    }

    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartAttack;
    }

    void StartAttack() {
        StartCoroutine(CreateEnemies());
    }

    IEnumerator CreateEnemies() {
        for (int i = 0; i < enemiesToCreate; i++) {
            Enemy go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoints[Random.Range(0, spawnerPoints.Length)].transform.position + upset, Quaternion.identity, enemyParent);
            yield return new WaitForSeconds(timeToSpawn);
        }

        yield return null;
    }
}
