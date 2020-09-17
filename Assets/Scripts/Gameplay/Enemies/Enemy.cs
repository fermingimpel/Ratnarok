using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected int damage;
    int index;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;

    Build buildToAttack;
    bool attackingBuild = false;
    protected virtual void Start() {
        Town.DestroyedTown += OnDie;
        transform.LookAt(transform.position + Vector3.right);
    }
    private void OnDisable() {
        Town.DestroyedTown -= OnDie;
    }

    private void Update() {
        if (attackingBuild)
            return;

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

    IEnumerator Attack() {
        attackingBuild = true;

        float t = 0;
        if (buildToAttack != null)
            while (t < timeToAttack && buildToAttack != null) {
                t += Time.deltaTime;
                yield return null;
            }

        if (buildToAttack != null) 
                buildToAttack.HitBuild(damage);

        StopCoroutine(Attack());
        ResetAttack();
        yield return null;
    }

    void ResetAttack() {
        if (attackingBuild && buildToAttack != null)
            StartCoroutine(Attack());
        else
            attackingBuild = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Build")) {
            buildToAttack = other.GetComponent<Build>();
            if (!attackingBuild)
                StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Build")) {
            attackingBuild = false;
            buildToAttack = null;
        }
    }

}
