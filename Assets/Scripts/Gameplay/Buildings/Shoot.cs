using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Enemy enemy;
    [SerializeField] int damage;
    float minX = -6f;

    private void Start() {
        Destroy(this.gameObject, 1f);
    }

    void Update() {
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
