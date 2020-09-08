using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    [SerializeField] protected List<Enemy> enemies;
    [SerializeField] protected int goldCost;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected bool defending;

    public delegate void BuildDestroyed(Build b);
    public static event BuildDestroyed DestroyedBuild;

    void Start() {
        GameplayManager.startEnemyAttack += StartDefend;
        GameplayManager.endEnemyAttack += StopDefend;
        StartDefend();
    }

    private void OnDisable() {
        GameplayManager.startEnemyAttack -= StartDefend;
        GameplayManager.endEnemyAttack -= StopDefend;
    }

    protected virtual void StopDefend() {
        Debug.Log("Yeah");
    }

    protected virtual void StartDefend() {
        Debug.Log("Yeah");
    }

    public virtual void HitBuild(int d) {
        health -= d;
        if (health <= 0) {
            if (DestroyedBuild != null)
                DestroyedBuild(this);
            Destroy(this.gameObject);
        }
    }

    public virtual void SetEnemyList(List<Enemy> list) {
        enemies = list;
    }
    public virtual int GetGoldCost() {
        return goldCost;
    }
}
