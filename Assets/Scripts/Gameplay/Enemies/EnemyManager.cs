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
        [Space]
        public float[] timeBetweenEveryEnemy;
    }

    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]

    [SerializeField] List<HordeManager> hordes;

    [SerializeField] float timeBetweenHordes;
    [SerializeField] int actualHorde;

    public delegate void UpdateHorde(int actualHorde, int maxHordes);
    public static event UpdateHorde HordeUpdate;

    bool allHordesCompleted = false;
    private void Awake() {
        GameplayManager.StartEnemyAttack += StartAttack;
        GameplayManager.EndEnemyAttack += StopAttack;
        Town.DestroyedTown += StopAttack;
        Enemy.Dead += RemoveEnemy;
    }
    void Start() {
        canCreateRandomEnemies = true;
        allHordesCompleted = false;
    }

    private void OnDisable() {
        GameplayManager.StartEnemyAttack -= StartAttack;
        GameplayManager.EndEnemyAttack -= StopAttack;
        Town.DestroyedTown -= StopAttack;
        Enemy.Dead -= RemoveEnemy;
    }

    void StartAttack() {
        attacking = true;
        StartCoroutine(Horde());
        if (HordeUpdate != null)
            HordeUpdate(actualHorde + 1, hordes.Count);
    }

    void StopAttack() {
        attacking = false;
    }

    IEnumerator Horde() {
        for (int l = 0; l < hordes.Count; l++) {
            actualHorde = l;
            if (HordeUpdate != null)
                HordeUpdate(actualHorde+1, hordes.Count);

            for (int i = 0; i < hordes[l].order.Length; i++) {
                for (int j = 0; j < hordes[l].cantOfEnemiesToCreateOfTypeInOrder[i]; j++) {
                    if (attacking) {
                        int spawn = UnityEngine.Random.Range(0, spawnerPoints.Length);
                        Enemy e = Instantiate(enemies[(int)hordes[l].order[i]], spawnerPoints[spawn].transform.position + upset, Quaternion.identity, enemyParent);
                        e.SetPath(paths[spawn].pos);
                        e.SetTown(town);
                        e.SetTypeOfEnemy((Enemy.Type)hordes[l].order[i]);
                        enemiesCreated.Add(e);

                        yield return new WaitForSeconds(hordes[l].timeBetweenEveryEnemy[i]);
                    }
                }
            }
            if (attacking)
                yield return new WaitForSeconds(timeBetweenHordes);
            else
                l = hordes.Count;

        }

        if (attacking)
            allHordesCompleted = true;

        yield return null;
    }

    public bool GetAllHordesCompleted() {
        if (allHordesCompleted && enemiesCreated.Count <= 0)         
            return true;
        
        return false;
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
