using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float damage;
    int index;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;

    Building buildToAttack;
    bool attackingBuild = false;

    [SerializeField] Town town;

    [SerializeField] List<Transform> path;
    [SerializeField] int actualPath = 0;
    [SerializeField] Vector3 posY;

    [SerializeField] Renderer rend;
    [SerializeField] Color normalColor;
    [SerializeField] Color hittedColor;
    bool hitted = false;

    [HideInInspector] public bool cheatsChangedHP = false;
    [SerializeField] Animator animator;
    public enum Type {
        Attacker,
        Tank,
        Bard,
        Bomberrat,
        None
    }

    public Type type;

    protected bool attackBuilds = true;
    bool townAttacked = false;

    [SerializeField] GameObject model;
    [SerializeField] BoxCollider bc;

    protected virtual void Start() {

        model.transform.position += new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        model.transform.LookAt(path[0].transform.position + posY);

        GameplayManager.EndEnemyAttack += OnDie;
        Town.DestroyedTown += OnDie;
    }
    private void OnDisable() {
        GameplayManager.EndEnemyAttack -= OnDie;
        Town.DestroyedTown -= OnDie;
    }
    protected virtual void Update() {
        if (attackingBuild)
            return;

        if (path[actualPath] != null) {
            transform.position = Vector3.MoveTowards(transform.position, path[actualPath].transform.position + posY, speed * Time.deltaTime);
            if (transform.position == path[actualPath].transform.position + posY) {
                actualPath++;
                if (actualPath == path.Count - 1)
                    AttackTown();
                else
                    model.transform.LookAt(path[actualPath].transform.position + posY);
            }
        }
    }

    public virtual void ReceiveDamage(float d) {
        health -= d;
        if (!hitted)
            StartCoroutine(Hit());
        if (health <= 0) {
            OnDie();
        }
    }

    public virtual void Heal(float h) {
        health += h;
        if (health >= maxHealth)
            health = maxHealth;
    }

    IEnumerator Hit() {
        hitted = true;
        rend.material.color = hittedColor;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = normalColor;
        StopCoroutine(Hit());
        hitted = false;
        yield return null;
    }
    protected void OnDie() {
        if (Dead != null)
            Dead(this);

        Destroy(this.gameObject);
    }
    public bool GetTownAttacked() {
        return townAttacked;
    }
    public void SetPath(List<Transform> p) {
        path = p;
    }
    IEnumerator Attack() {
        attackingBuild = true;

        float t = 0;
        if(animator!=null && buildToAttack != null)
            animator.Play("Attack");
        if (buildToAttack != null)
            while (t < timeToAttack && buildToAttack != null) {
                t += Time.deltaTime;
                yield return null;
            }

        if (buildToAttack != null) {
            AkSoundEngine.PostEvent("enemy_attack", this.gameObject);
            buildToAttack.HitBuild(damage);
        }
        ResetAttack();
        yield return null;
    }

    public void SetTown(Town t) {
        town = t;
    }

    void ResetAttack() {
        if (attackingBuild && buildToAttack != null)
            StartCoroutine(Attack());
        else {
            bc.enabled = false;
            attackingBuild = false;
            bc.enabled = true;
        }
    }

    void AttackTown() {
        if (town != null)
            town.ReceiveDamage(damage*2f);
        townAttacked = true;
        OnDie();
    }

    private void OnTriggerEnter(Collider other) {
        if (!attackBuilds)
            return;

        if (other.gameObject.CompareTag("Structure")) {
            buildToAttack = other.GetComponent<Building>();
            if (!attackingBuild)
                StartCoroutine(Attack());
            return;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!attackBuilds)
            return;

        if (other.gameObject.CompareTag("Structure")) {
            attackingBuild = false;
            buildToAttack = null;
        }
    }

    public void SetTypeOfEnemy(Type t) {
        type = t;
    }
    public Type GetTypeOfEnemy() {
        return type;
    }

    // [SerializeField] protected int health;
    // int maxHealth;
    // [SerializeField] protected int speed;
    // [SerializeField] protected float timeToAttack;
    // [SerializeField] protected int damage;

    public void SetDamage(float d) {
        damage = d;
    }
    public float GetDamage() {
        return damage;
    }

    public void SetMaxHealth(float mh) {
        cheatsChangedHP = true;
        health = mh;
        maxHealth = mh;
    }
    public float GetMaxHealth() {
        return maxHealth;
    }

    public void SetSpeed(float s) {
        speed = s;
    }
    public float GetSpeed() {
        return speed;
    }

    public void SetTimeToAttack(float tta) {
        timeToAttack = tta;
    }
    public float GetTimeToAttack() {
        return timeToAttack;
    }

}

