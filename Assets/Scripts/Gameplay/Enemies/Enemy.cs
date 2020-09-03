using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] protected Town town;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;

    protected virtual void Start() {
        town = FindObjectOfType<Town>();
        if (town != null)
            agent.destination = town.transform.position;
        GameplayManager.endEnemyAttack += OnDie;
    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= OnDie;
    }

    public virtual void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            OnDie();
        }
    }

    protected void OnDie() {
        if (Dead != null)
            Dead(this);

        Destroy(this.gameObject);
    }

    protected virtual void AttackTown() {
        town.ReceiveDamage(damage);
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other) {
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
