using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    [SerializeField] protected int toolsCost;
    [SerializeField] protected float preparationTime;
    [SerializeField] protected float cooldownToCreate;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] int index;

    bool attacking = false;

    public delegate void BuildDestroyed(Building b);
    public static event BuildDestroyed DestroyedBuild;

    [SerializeField] List<Transform> path;
    [SerializeField] protected Vector3 lookPos;

    [SerializeField] List<Renderer> rends;
    bool hitted = false;
    [SerializeField] Color hittedColor;
    [SerializeField] Color normalColor;
    [HideInInspector] public bool cheatsChangedHP = false;

    public enum Type {
        Cannon,
        ToolsGenerator,
        Fence,
        Catapult,
        Flamethrower,
        Crossbow,
        None
    }
    public Type type;
    protected virtual void Start() {
        // GameplayManager.startEnemyAttack += StartDefend;
        GameplayManager.EndEnemyAttack += StopDefend;
        StartDefend();
    }

    private void OnDisable() {
        //GameplayManager.startEnemyAttack -= StartDefend;
        GameplayManager.EndEnemyAttack -= StopDefend;
    }
    private void OnDestroy() {
        GameplayManager.EndEnemyAttack -= StopDefend;
        if (DestroyedBuild != null)
            DestroyedBuild(this);
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
        if (!hitted)
            StartCoroutine(Hit());
        if (health <= 0) {
            if (DestroyedBuild != null)
                DestroyedBuild(this);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Hit() {
        hitted = true;
        for(int i=0;i<rends.Count;i++)
            if(rends[i]!=null)
                rends[i].material.color = hittedColor;
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < rends.Count; i++)
            if (rends[i] != null)
                rends[i].material.color = normalColor;
        StopCoroutine(Hit());
        hitted = false;
        yield return null;
    }

    public void SetToolsCost(int tc) {
        toolsCost = tc;
    }
    public int GetToolsCost() {
        return toolsCost;
    }
    public void SetHP(int hp) {
        cheatsChangedHP = true;
        health = hp;
    }
    public int GetHP() {
        return health;
    }
    public void SetDamage(int d) {
        damage = d;
    }
    public int GetDamage() {
        return damage;
    }
    public void SetPreparationTime(float t) {
        preparationTime = t;
    }
    public float GetPreparationTime() {
        return preparationTime;
    }
    public void SetType(Type t) {
        type = t;
    }
    public Type GetTypeOfBuild() {
        return type;
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
