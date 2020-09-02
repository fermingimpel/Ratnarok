using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    [SerializeField] protected List<Enemy> enemies;
    [SerializeField] protected int goldCost;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected int damage;
    void Start() {
        GameplayManager.startEnemyAttack += StartDefend;
        GameplayManager.endEnemyAttack += StopDefend;
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

    public virtual void SetEnemyList(List<Enemy> list) {
        enemies = list;
    }
    public virtual int GetGoldCost() {
        return goldCost;
    }
}
