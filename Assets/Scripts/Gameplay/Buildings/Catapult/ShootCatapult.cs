using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCatapult : Shoot {
    [SerializeField] Vector3 initPos;
    [SerializeField] Vector3 finalPos;
    [SerializeField] Vector3 midPos;
    float t = 0;
    void Start() {
        initPos = transform.position;
        finalPos = direction * 20 + initPos;
        midPos = Vector3.Lerp(initPos, finalPos, 0.33f) + (Vector3.up * 1.5f);

        StartCoroutine(Move());
    }

    IEnumerator Move() {
        while (transform.position != midPos) {
            transform.position = Vector3.MoveTowards(transform.position, midPos, speed * Time.deltaTime);
            yield return null;
        }

        while (transform.position != finalPos) {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(Move());
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(this.gameObject, 0.05f);
        }
    }
}
