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

    bool attacking ;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    void Start() {
        GameplayManager.StartEnemyAttack += StartAttack;
        GameplayManager.EndEnemyAttack += StopAttack;
    }

    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartAttack;
        GameplayManager.EndEnemyAttack -= StopAttack;
    }

    void StartAttack() {
        attacking = true;
        StartCoroutine(CreateEnemies());
    }

    void StopAttack() {
        attacking = false;
        StopCoroutine(CreateEnemies());
    }

    IEnumerator CreateEnemies() {
        int enemiesCreated = 0;
        while(attacking && enemiesCreated < enemiesToCreate) { 
            Enemy go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoints[Random.Range(0, spawnerPoints.Length)].transform.position + upset, Quaternion.identity, enemyParent);
            yield return new WaitForSeconds(timeToSpawn);
        }

        yield return null;
    }
}
