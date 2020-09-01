using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Enemy enemy;
    [SerializeField] int damage;

    private void Start() {
        Destroy(this.gameObject, 1f);
    }

    void Update() {
        if (enemy == null)
            Destroy(this.gameObject);

        if (enemy != null)
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void SetObjective(Enemy e) {
        enemy = e;
    }

    public void SetDamage(int d) {
        damage = d;
    }
}
