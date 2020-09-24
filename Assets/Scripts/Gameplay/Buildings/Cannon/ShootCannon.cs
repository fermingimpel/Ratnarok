using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : Shoot {
    void Start() {

    }
    protected void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < minX)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
