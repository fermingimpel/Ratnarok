using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCannon : Bullet {
    bool hitSomething = false;

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (transform.position == direction)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            if (!hitSomething) {
                hitSomething = true;
                other.GetComponent<Enemy>().ReceiveDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}