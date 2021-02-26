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

    public delegate void EnemyCreated(Enemy e);
    public static event EnemyCreated CreatedEnemy;

    [SerializeField] List<Enemy> enemiesCreated;


    [Serializable]
    public class HordeManager {
        public enum Order {
            Attacker, Tank, Bomberrat, Bard, Acid
        }
        public Order[] order;
        [Space]
        public int[] cantOfEnemiesToCreateOfTypeInOrder;
        [Space]
        public float[] timeBetweenEveryEnemy;
    }

    [SerializeField] List<HordeManager> hordes;
    [SerializeField] float timeBetweenHordes;
    [SerializeField] int actualHorde;
    [SerializeField] int enemiesRemaining;
    List<float> enemiesSpawnTimers;
    List<int> enemiesOfOrderCreated;
    bool changingHorde = false;
    bool allHordesCompleted;

    public delegate void UpdateHorde(int actualHorde, int maxHordes);
    public static event UpdateHorde HordeUpdate;

    public static Action AllEnemiesEliminated;
    private void Start() {
        allHordesCompleted = false;
        
        enemiesSpawnTimers = new List<float>();
        enemiesOfOrderCreated = new List<int>();
        for (int i = 0; i < hordes[actualHorde].order.Length; i++) {
            enemiesSpawnTimers.Add(0f);
            enemiesOfOrderCreated.Add(0);
        }
        Enemy.Dead += RemoveEnemy;

        for(int i = 0; i < hordes.Count; i++) 
            for(int j = 0; j < hordes[i].cantOfEnemiesToCreateOfTypeInOrder.Length; j++) 
                enemiesRemaining += hordes[i].cantOfEnemiesToCreateOfTypeInOrder[j];

        if (HordeUpdate != null)
            HordeUpdate(actualHorde+1, hordes.Count);
    }
    private void OnDisable() {
        Enemy.Dead -= RemoveEnemy;
    }

    private void Update() {
        if (!attacking)
            return;

        for (int i = 0; i < hordes[actualHorde].order.Length; i++) {
            enemiesSpawnTimers[i] += Time.deltaTime;
            if (enemiesSpawnTimers[i] >= hordes[actualHorde].timeBetweenEveryEnemy[i] && enemiesOfOrderCreated[i] < hordes[actualHorde].cantOfEnemiesToCreateOfTypeInOrder[i]) {
                SpawnEnemy(actualHorde, i);
                enemiesSpawnTimers[i] = 0f;
                enemiesOfOrderCreated[i]++;
            }
        }

        for (int i = 0; i < hordes[actualHorde].order.Length; i++)
            if (enemiesOfOrderCreated[i] < hordes[actualHorde].cantOfEnemiesToCreateOfTypeInOrder[i] || enemiesCreated.Count > 0)
                return;

        if (!changingHorde)
            StartCoroutine(ChangeHorde());
    }

    IEnumerator ChangeHorde() {
        changingHorde = true;
        attacking = false;
        actualHorde++;
        if (actualHorde >= hordes.Count) {
            allHordesCompleted = true;
        }
        else {
            yield return new WaitForSeconds(timeBetweenHordes);
            enemiesSpawnTimers.Clear();
            enemiesOfOrderCreated.Clear();

            for(int i=0;i<hordes[actualHorde].order.Length;i++) {
                enemiesSpawnTimers.Add(0f);
                enemiesOfOrderCreated.Add(0);
            }
            attacking = true;
            if (HordeUpdate != null)
                HordeUpdate(actualHorde + 1, hordes.Count);
        }
        changingHorde = false;
        yield return null;
    }

    void SpawnEnemy(int horde, int order) {
        if (attacking && !allHordesCompleted) {
            int spawn = UnityEngine.Random.Range(0, spawnerPoints.Length);
            Enemy e = Instantiate(enemies[(int)hordes[horde].order[order]], spawnerPoints[spawn].transform.position + upset, Quaternion.identity, enemyParent);
            e.SetPath(paths[spawn].pos);
            e.SetTown(town);
            enemiesCreated.Add(e);
            enemiesRemaining--;
        }
    }
    void RemoveEnemy(Enemy e) {
       enemiesCreated.Remove(e);
    
        if(enemiesRemaining <= 0 && enemiesCreated.Count <= 0)
            if (AllEnemiesEliminated != null)
                AllEnemiesEliminated();
    }
    public void StartAttack() {
        attacking = true;
        allHordesCompleted = false;
    }
    public void PauseAttack() {
        attacking = false;
        for (int i = 0; i < enemiesCreated.Count; i++)
            if (enemiesCreated[i] != null)
                enemiesCreated[i].attacking = false;
    }
    public void ResumeAttack() {
        attacking = true;
        for (int i = 0; i < enemiesCreated.Count; i++)
            if (enemiesCreated[i] != null)
                enemiesCreated[i].attacking = true;
    }
    public void StopAttack() {
        attacking = false;
        for(int i = 0; i < enemiesCreated.Count; i++) {
            if(enemiesCreated[i]!=null)
                Destroy(enemiesCreated[i].gameObject);
        }
        enemiesCreated.Clear();
    }
}
