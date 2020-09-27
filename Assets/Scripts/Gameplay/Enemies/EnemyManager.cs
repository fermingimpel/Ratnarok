using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] GameObject[] spawnerPoints;

    [Serializable]
    public class Paths {
        public List<Transform> pos;
    }

    [SerializeField] List<Paths> paths;

    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;

    [SerializeField] int enemiesToCreate;
    [SerializeField] float timeToSpawn;
    [SerializeField] Town town;
    bool attacking;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    void Start() {
        GameplayManager.StartEnemyAttack += StartAttack;
        GameplayManager.EndEnemyAttack += StopAttack;
        Town.DestroyedTown += StopAttack;
    }

    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartAttack;
        GameplayManager.EndEnemyAttack -= StopAttack;
        Town.DestroyedTown -= StopAttack;
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
            int spawn = UnityEngine.Random.Range(0, spawnerPoints.Length);
            Enemy go = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], spawnerPoints[spawn].transform.position + upset, Quaternion.identity, enemyParent);
            go.SetPath(paths[spawn].pos);
            go.SetTown(town);
            yield return new WaitForSeconds(timeToSpawn);
        }

        yield return null;
    }
}
