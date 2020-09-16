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
        //GameplayManager.endEnemyAttack += OnDie;
        Town.DestroyedTown += OnDie;
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        StopCoroutine(LateStart());
        yield return null;
    }
    private void OnDisable() {
        //GameplayManager.endEnemyAttack -= OnDie;
        Town.DestroyedTown -= OnDie;
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
    protected IEnumerator AttackObjective() {
        StopCoroutine(AttackObjective());
        yield return null;
    }
}
