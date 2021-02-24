using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpread : MonoBehaviour {
    [SerializeField] public float timeBetweenHits;
    [SerializeField] BoxCollider bc;
    int actualHits = 0;
    [SerializeField] public int maxHits;
    [SerializeField] float damagePerHit;

    private void OnEnable() {
        StartCoroutine(WaitToAttack());
    }
    private void OnDisable() {
        StopCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack() {
        actualHits = 0;
        while (actualHits < maxHits) {
            yield return new WaitForSeconds(timeBetweenHits);
            Attack();
            actualHits++;
        }
    }

    void Attack() {
        bc.enabled = false;
        bc.enabled = true;
    }
   
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Structure")) {
            other.GetComponent<Structure>().HitStructure(damagePerHit);
        }
    }
}
