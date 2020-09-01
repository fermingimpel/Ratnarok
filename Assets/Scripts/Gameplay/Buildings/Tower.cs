using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Shoot shoot;
    [SerializeField] Vector3 upset;
    [SerializeField] float timeToAttack;
    [SerializeField] float distanceToAttack;
    [SerializeField] int damage;
    void Start() {
        GameplayManager.startEnemyAttack += StartDefend;
        GameplayManager.endEnemyAttack += StopDefend;

        StartCoroutine(PrepareAttack());
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartDefend;
        GameplayManager.endEnemyAttack -= StopDefend;

    }

    void StopDefend() {
        StopCoroutine(PrepareAttack());
    }

    void StartDefend() {
        StartCoroutine(PrepareAttack());
    }

    public void SetEnemyList(List<GameObject> list) {
        enemies = list;
    }

    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(timeToAttack);
        int index = 0;
        float nearest = 999999;
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] != null)
                if (Vector3.Distance(transform.position, enemies[i].transform.position) < nearest) {
                    index = i;
                    nearest = Vector3.Distance(transform.position, enemies[i].transform.position);
                }
        }
        StopCoroutine(PrepareAttack());
        Attack(index, nearest);
        yield return null;
    }
    void Attack(int ind, float dis) {
        if (enemies[ind] != null && dis < distanceToAttack) {
            Shoot s = Instantiate(shoot, transform.position + upset, Quaternion.identity);
            s.SetObjective(enemies[ind].GetComponent<Enemy>());
            s.SetDamage(damage);
        }
        StartCoroutine(PrepareAttack());
    }
}
