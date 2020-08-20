using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] Enemy enemy;

    private void Start() {
        Destroy(this.gameObject, 1f);
    }

    void Update() {
        if (enemy != null)
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<Enemy>().ReceiveDamage(2);
            Destroy(this.gameObject);
        }
    }

    public void SetObjective(Enemy e) {
        enemy = e;
    }
}
