using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    [SerializeField] protected int toolsCost;
    [SerializeField] protected float preparationTime;
    [SerializeField] protected float cooldownToCreate;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] int index;

    public delegate void BuildDestroyed(Build b);
    public static event BuildDestroyed DestroyedBuild;

    void Start() {
       // GameplayManager.startEnemyAttack += StartDefend;
       // GameplayManager.endEnemyAttack += StopDefend;
        StartDefend();
    }

    private void OnDisable() {
        //GameplayManager.startEnemyAttack -= StartDefend;
        //GameplayManager.endEnemyAttack -= StopDefend;
    }

    protected virtual void StopDefend() {
        Debug.Log("Stop Defend");
    }

    protected virtual void StartDefend() {
        Debug.Log("Start Defend");
    }

    protected virtual void Attack() {
        Debug.Log("Attack");
    }

    public virtual void HitBuild(int d) {
        health -= d;
        if (health <= 0) {
            if (DestroyedBuild != null)
                DestroyedBuild(this);
            Destroy(this.gameObject);
        }
    }
    public virtual int GetToolsCost() {
        return toolsCost;
    }
    public virtual int GetHP() {
        return health;
    }
    public virtual int GetDamage() {
        return damage;
    }
}
