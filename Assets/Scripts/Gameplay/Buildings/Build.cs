using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    [SerializeField] protected List<Enemy> enemies;
    [SerializeField] protected int goldCost;
    [SerializeField] protected float baseTimeToAttack;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float distanceToAttack;
    [SerializeField] protected float health;
    [SerializeField] protected int damage;
    [SerializeField] protected bool defending;
    [SerializeField] protected bool inFury;

    float maxHealth;
    BuildingCreator bc;

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
        float goldToUse = 0;
        if (bc != null) {
            goldToUse = goldCost - ((health / maxHealth) * goldCost);
            //Debug.Log(goldToUse);
            //Debug.Log(goldCost);
           //Debug.Log("Health: " + health);
           //Debug.Log("Max Health: " + maxHealth);
           //Debug.Log("Health / maxHealth: " + (health / maxHealth));
           // Debug.Log((health / maxHealth) * goldCost);
            //Debug.Log(70 * 0.8f);
            //Debug.Log(0.8f * 70);
            if(bc.GetGold() >= goldToUse) {
                health = maxHealth;
                bc.UseGold((int)goldToUse);
            }
        }
    }
    public void SetBuildCreator(BuildingCreator buildc) {
        bc = buildc;
    }
    public virtual void SetEnemyList(List<Enemy> list) {
        enemies = list;
    }
    public virtual int GetGoldCost() {
        return goldCost;
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
