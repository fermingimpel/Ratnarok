using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCrossbow : Bullet {
    private void Start() {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        StartCoroutine(Move());
        StopCoroutine(LateStart());
        yield return null;
    }

    IEnumerator Move() {
        while (transform.position != direction) {
            transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            yield return null;
        }
        Destroy(this.gameObject);
        StopCoroutine(Move());
        yield return null;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy")) {
            other.GetComponentInParent<Enemy>().ReceiveDamage(damage);
        }
    }
}
