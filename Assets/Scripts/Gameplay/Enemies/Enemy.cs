using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] Town town;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int health;
    [SerializeField] int damage;

    public delegate void EnemyDead(GameObject e);
    public static event EnemyDead Dead;

    public enum Type {
        Attacker,
        Tank
    }

    void Start() {
        town = FindObjectOfType<Town>();
        if (town != null)
            agent.destination = town.transform.position;
    }

    public void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            if (Dead != null)
                Dead(this.gameObject);

            Destroy(this.gameObject);
        }
    }

    void AttackTown() {
        town.ReceiveDamage(damage);
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Town":
                AttackTown();
                break;
            case "Fence":
                break;
            default:
                break;
        }
    }

}
