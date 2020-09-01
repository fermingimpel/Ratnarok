using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject[] spawnerPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;
    bool attacking = true;

    public delegate void EnemyCreated(GameObject enemy);
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
        StartCoroutine(PrepareEnemy());
    }
    void StopSpawning() {
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

        for(int i = 0; i < spawnerPoints.Length; i++) {
            GameObject go = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoints[i].transform.position, Quaternion.identity, enemyParent);
            if (CreatedEnemy != null)
                CreatedEnemy(go);
        }
       
        StartCoroutine(PrepareEnemy());
    }
}
