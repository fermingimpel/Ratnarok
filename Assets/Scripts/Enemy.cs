using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] Town town;
    // Start is called before the first frame update
    void Start() {
        town = FindObjectOfType<Town>();
        GameplayManager.endEnemyAttack += StopAttack;
    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= StopAttack;
    }

    void StopAttack() {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (town != null)
            transform.position = Vector3.MoveTowards(transform.position, town.transform.position, speed * Time.deltaTime);
    }

    public void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
