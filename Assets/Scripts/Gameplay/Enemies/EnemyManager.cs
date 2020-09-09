using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] List<Build> builds;
    [SerializeField] GameObject[] spawnerPoints;
    [SerializeField] Enemy[] enemies;
    [SerializeField] Transform enemyParent;
    [SerializeField] Vector3 upset;
    bool attacking = true;

    [SerializeField] int hordes;
    [SerializeField] float timeToSpawn;
    [SerializeField] float timeToHorde;

    [SerializeField] Town town;

    public delegate void EnemyCreated(Enemy enemy);
    public static event EnemyCreated CreatedEnemy;

    void Start() {
        StopCoroutine(PrepareEnemy());
        StopCoroutine(CreateEnemies()); 
        BuildingCreator.ChangedBuilds += SetBuildsList;
        GameplayManager.startEnemyAttack += StartSpawning;
        GameplayManager.endEnemyAttack += StopSpawning;
        Town.DestroyedTown += DestroyedTown;
        Build.DestroyedBuild -= DestroyedBuild;
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartSpawning;
        GameplayManager.endEnemyAttack -= StopSpawning;
        BuildingCreator.ChangedBuilds -= SetBuildsList;
        Town.DestroyedTown -= DestroyedTown;
        Build.DestroyedBuild -= DestroyedBuild;
    }

    void SetBuildsList(List<Build> b) {
        builds = b;
    }

    void StartSpawning() {
        attacking = true;
        StartCoroutine(CreateEnemies());
    }
    void StopSpawning() {
        attacking = false;
        StopCoroutine(PrepareEnemy());
        StopCoroutine(CreateEnemies());
    }

    void DestroyedTown() {
        town = null;
        attacking = false;
        this.enabled = false;
        StopCoroutine(PrepareEnemy());
        StopCoroutine(CreateEnemies());
    }

    void DestroyedBuild(Build b) {
        builds.Remove(b);
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
                if (town != null)
                    go.SetTown(town);

                go.SetBuildsList(builds);
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
