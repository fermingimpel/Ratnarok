using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] BoxCollider bc;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Shoot shoot;
    [SerializeField] Vector3 upset;
    void Start() {
        enemies.Add(null);
      //  bc.enabled = false;
        GameplayManager.startEnemyAttack += StartDefend;
        GameplayManager.endEnemyAttack += StopDefend;
        StartCoroutine(PrepareAttack());
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartDefend;
        GameplayManager.endEnemyAttack -= StopDefend;
    }

    void StopDefend() {
        enemies.Clear();
        enemies.Add(null);
        bc.enabled = false;
        StopCoroutine(PrepareAttack());
    }

    void StartDefend() {
        enemies.Clear();
        enemies.Add(null);
        bc.enabled = true;
        StartCoroutine(PrepareAttack());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Enemy")
            return;

        enemies.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy")
            return;

        enemies.Remove(other.gameObject);
    }

    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(0.33f);
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
        Attack(index);
        yield return null;
    }
    void Attack(int ind) {
        if (enemies[ind] != null) {
            Shoot s = Instantiate(shoot, transform.position + upset, Quaternion.identity);
            s.SetObjective(enemies[ind].GetComponent<Enemy>());
        }
        StartCoroutine(PrepareAttack());
    }
}
