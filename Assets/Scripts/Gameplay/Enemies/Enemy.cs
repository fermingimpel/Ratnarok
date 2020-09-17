using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected float timeToAttack;
    int index;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;

    protected virtual void Start() {
        Town.DestroyedTown += OnDie;
        transform.LookAt(transform.position + Vector3.right);
    }
    private void OnDisable() {
        Town.DestroyedTown -= OnDie;
    }

    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
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

}
