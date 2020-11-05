using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {
    [SerializeField] protected float toolsCost;
    [SerializeField] protected float preparationTime;
    [SerializeField] protected float cooldownToCreate;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] int index;
    float maxHealth;

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

    [SerializeField] GameObject canvas;
    [SerializeField] Image hpBar;

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
       maxHealth = health;
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

    private void LateUpdate() {
        if (canvas != null) {
            canvas.transform.LookAt(Camera.main.transform);
            canvas.transform.Rotate(0f, 180f, 0f);
        }
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
 
    public virtual void HitBuild(float d) {
        health -= d;
        if (hpBar != null) {
            hpBar.fillAmount = health/maxHealth;
        }
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

    public void SetToolsCost(float tc) {
        toolsCost = tc;
    }
    public float GetToolsCost() {
        return toolsCost;
    }
    public void SetHP(float hp) {
        cheatsChangedHP = true;
        health = hp;
    }
    public float GetHP() {
        return health;
    }
    public void SetDamage(float d) {
        damage = d;
    }
    public float GetDamage() {
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
            lookPos = new Vector3(path[la].position.x, transform.position.y, path[la].position.z);        }
    }





}
