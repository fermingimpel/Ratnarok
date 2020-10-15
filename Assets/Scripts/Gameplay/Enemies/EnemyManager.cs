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

    [SerializeField] float timeToSpawn;
    [SerializeField] Town town;
    bool attacking;

    [SerializeField] int enemiesInHorde;
    [SerializeField] float timeToSpawnEnemiesInHorde;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    [SerializeField] List<Enemy> enemiesCreated;

    void Start() {
        GameplayManager.StartEnemyAttack += StartAttack;
        GameplayManager.EndEnemyAttack += StopAttack;
        GameplayManager.StartAttackHorde += StartHorde;
        Town.DestroyedTown += StopAttack;
        Enemy.Dead += RemoveEnemy;
    }

    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartAttack;
        GameplayManager.EndEnemyAttack -= StopAttack;
        GameplayManager.StartAttackHorde -= StartHorde;
        Town.DestroyedTown -= StopAttack;
        Enemy.Dead -= RemoveEnemy;
    }

    void StartAttack() {
        attacking = true;
        StartCoroutine(CreateEnemies());
    }

    void StopAttack() {
        attacking = false;
        StopCoroutine(CreateEnemies());
    }

    void StartHorde() {
        StartCoroutine(Horde());
    }
    IEnumerator Horde() {
        int enemiesCreated = 0;
        while(enemiesCreated < enemiesInHorde) {
            SpawnEnemy();
            enemiesCreated++;
            yield return new WaitForSeconds(timeToSpawnEnemiesInHorde);
        }

        StopCoroutine(Horde());
        yield return null;
    }
    IEnumerator CreateEnemies() {
        while(attacking) {
            SpawnEnemy();
            yield return new WaitForSeconds(timeToSpawn);
        }

        yield return null;
    }

    void SpawnEnemy() {
        int spawn = UnityEngine.Random.Range(0, spawnerPoints.Length);
        int type = UnityEngine.Random.Range(0, enemies.Length);
        Enemy go = Instantiate(enemies[type], spawnerPoints[spawn].transform.position + upset, Quaternion.identity, enemyParent);
        go.SetPath(paths[spawn].pos);
        go.SetTown(town);
        go.SetTypeOfEnemy((Enemy.Type)type);
        enemiesCreated.Add(go);
    }

    void RemoveEnemy(Enemy e) {
        enemiesCreated.Remove(e);
    }

    public List<Enemy> GetEnemies() {
        return enemiesCreated;
    }

}
