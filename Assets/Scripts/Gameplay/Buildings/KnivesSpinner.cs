using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnivesSpinner : Build {
    [SerializeField] GameObject[] knives;
    [SerializeField] float rotationSpeed;

    protected override void StopDefend() {
        StopCoroutine(PrepareAttack());
    }
    protected override void StartDefend() {
        StartCoroutine(PrepareAttack());
    }

    void Update() {
        for (int i = 0; i < knives.Length; i++)
            knives[i].transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(timeToAttack);
        StopCoroutine(PrepareAttack());
        Attack();
        yield return null;
    }
    void Attack() {
        for(int i = 0; i < enemies.Count; i++) {
            if (enemies[i] != null)
                if (Vector3.Distance(transform.position, enemies[i].transform.position) < distanceToAttack) {
                    enemies[i].ReceiveDamage(damage);
                }
        }

        StartCoroutine(PrepareAttack());
    }

}
