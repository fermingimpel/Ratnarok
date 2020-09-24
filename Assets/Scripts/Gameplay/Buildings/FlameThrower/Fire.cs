using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Shoot {
    [SerializeField] int maxTicks;
    [SerializeField] float timeBetweenTicks;
    int actualTicks = 0;
    [SerializeField] BoxCollider bc;

    private void OnEnable() {
        StartCoroutine(WaitToAttack());
    }

    private void OnDisable() {
        StopCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack() {
        actualTicks = 0;
            Debug.Log(actualTicks);
        while (actualTicks < maxTicks) {
            yield return new WaitForSeconds(timeBetweenTicks);
            Attack();
            actualTicks++;
            Debug.Log(actualTicks);
        }

        StopCoroutine(WaitToAttack());
    }

    void Attack() {
        bc.enabled = false;
        bc.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("XD");
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
        }
    }

}
