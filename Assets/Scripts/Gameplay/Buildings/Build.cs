using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    [SerializeField] protected List<Enemy> enemies;
    [SerializeField] protected int cheeseCost;
    [SerializeField] protected float baseTimeToAttack;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected float health;
    [SerializeField] protected int damage;
    [SerializeField] protected bool defending;
    [SerializeField] protected bool inFury;

    float maxHealth;
    BuildingCreator buildCreator;

    public delegate void BuildDestroyed(Build b);
    public static event BuildDestroyed DestroyedBuild;

    void Start() {
        maxHealth = health;
        timeToAttack = baseTimeToAttack;
       // GameplayManager.startEnemyAttack += StartDefend;
       // GameplayManager.endEnemyAttack += StopDefend;
        StartDefend();
    }

    private void OnDisable() {
        //GameplayManager.startEnemyAttack -= StartDefend;
        //GameplayManager.endEnemyAttack -= StopDefend;
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

    public void SetFury(bool f) {
        inFury = f;
        if (f)
            timeToAttack /= 2;
        else
            timeToAttack = baseTimeToAttack;
    }

    public void RepairBuild() {
        float cheeseToUse = 0;
        if (buildCreator != null) {
            cheeseToUse = cheeseCost - ((health / maxHealth) * cheeseCost);
            if(buildCreator.GetCheese() >= cheeseToUse) {
                health = maxHealth;
                buildCreator.UseCheese((int)cheeseToUse);
            }
        }
    }

    public void DestroyBuild() {
        if (buildCreator != null) {
            int goldToReturn = cheeseCost / 2;
            buildCreator.AddCheese(goldToReturn);
            if (DestroyedBuild != null)
                DestroyedBuild(this);
            Destroy(this.gameObject);
        }
    }

    public void SetBuildCreator(BuildingCreator buildc) {
        buildCreator = buildc;
    }
    public virtual void SetEnemyList(List<Enemy> list) {
        enemies = list;
    }
    public virtual int GetCheeseCost() {
        return cheeseCost;
    }
    public virtual float GetAttackSpeed() {
        return timeToAttack;
    }
    public virtual int GetHP() {
        return (int)health;
    }
    public virtual int GetDamage() {
        return damage;
    }
}
