using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCrossbow : Bullet {

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        if (transform.position == direction)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
        }
    }
}
