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

    bool canCreateRandomEnemies = true;
    int type = 0;

    [Serializable]
    public class HordeManager {
        public enum Order {
            Attacker, Tank, Bard, Bomberrat
        }
        public Order[] order;
        [Space]
        public int[] cantOfEnemiesToCreateOfTypeInOrder;

    }
    [SerializeField] List<HordeManager> hordes;

    void Start() {
        GameplayManager.StartEnemyAttack += StartAttack;
        GameplayManager.EndEnemyAttack += StopAttack;
        GameplayManager.StartAttackHorde += StartHorde;
        Town.DestroyedTown += StopAttack;
        Enemy.Dead += RemoveEnemy;
        canCreateRandomEnemies = true;
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

    void StartHorde(int h) {
        StartCoroutine(Horde(h));
    }
    IEnumerator Horde(int h) {
        attacking = false;
        for(int i = 0; i < hordes[h].order.Length; i++) {
            for (int j = 0; j < hordes[h].cantOfEnemiesToCreateOfTypeInOrder[i]; j++) {
                for (int k = 0; k < spawnerPoints.Length; k++) {
                    Enemy e = Instantiate(enemies[(int)hordes[h].order[i]], spawnerPoints[k].transform.position + upset, Quaternion.identity, enemyParent);
                    e.SetPath(paths[k].pos);
                    e.SetTown(town);
                    e.SetTypeOfEnemy((Enemy.Type)hordes[h].order[i]);
                    enemiesCreated.Add(e);
                }
                yield return new WaitForSeconds(timeToSpawnEnemiesInHorde);
            }

           // yield return new WaitForSeconds(timeToSpawnEnemiesInHorde);
        }
        attacking = true;
        StartCoroutine(CreateEnemies());
        StopCoroutine(Horde(h));
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
        if(canCreateRandomEnemies)
            type = UnityEngine.Random.Range(0, enemies.Length);
        Enemy go = Instantiate(enemies[type], spawnerPoints[spawn].transform.position + upset, Quaternion.identity, enemyParent);
        go.SetPath(paths[spawn].pos);
        go.SetTown(town);
        go.SetTypeOfEnemy((Enemy.Type)type);
        enemiesCreated.Add(go);
    }
    public void SetOnlyOneEnemyToCreate(int t) {
        type = t;
        canCreateRandomEnemies = false;
    }
    public void SetCreateRandomEnemies() {
        canCreateRandomEnemies = true;
    }
    void RemoveEnemy(Enemy e) {
        enemiesCreated.Remove(e);
    }

    public List<Enemy> GetEnemies() {
        return enemiesCreated;
    }

}
