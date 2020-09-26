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

    [SerializeField] List<Transform> path;
    [SerializeField] protected Vector3 lookPos;
    void Start() {
       // GameplayManager.startEnemyAttack += StartDefend;
       // GameplayManager.endEnemyAttack += StopDefend;
        StartDefend();
    }

    private void OnDisable() {
        //GameplayManager.startEnemyAttack -= StartDefend;
        //GameplayManager.endEnemyAttack -= StopDefend;
    }
    private void Update() {
       
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
    public void SetPath(List<Transform> p) {
        path = p;
    }
    public void SetLookAt(int la) {
        if (path[la] != null) {
            transform.LookAt(new Vector3(path[la].position.x, transform.position.y, path[la].position.z));
            lookPos = new Vector3(path[la].position.x, transform.position.y, path[la].position.z);
        }
    }
}
