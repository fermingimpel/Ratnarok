using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeToAttack;
    [SerializeField] protected float timerToAttack = 0;
    [SerializeField] protected float damage;
    [SerializeField] protected Structure structureToAttack;
    protected bool attackingStructure = false;
    public bool attacking = true;

    [SerializeField] Town town;

    [SerializeField] List<Transform> path;
    [SerializeField] int actualPath = 0;
    [SerializeField] Vector3 posY;

    [SerializeField] Renderer rend;
    [SerializeField] Color normalColor;
    [SerializeField] Color hittedColor;
    bool hitted = false;

    [SerializeField] protected Animator animator;

    public enum Type {
        Attacker,
        Tank,
        Bard,
        Bomberrat,
        Acid,
        None
    }

    public Type type;

    [SerializeField] protected bool attackBuilds = true;
    bool townAttacked = false;

    [SerializeField] GameObject model;
    [SerializeField] protected BoxCollider bc;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;

    [SerializeField] CheeseMoney cheeseCoin;

    protected virtual void Start() {
        model.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
        model.transform.LookAt(path[0].transform.position + posY);
        MovementAnimation();
    }

    protected virtual void Update() {
        if (!attacking)
            return;

        if (attackingStructure)
            return;

        if (path[actualPath] != null) {
            transform.position = Vector3.MoveTowards(transform.position, path[actualPath].transform.position + posY, speed * Time.deltaTime);
            if (transform.position == path[actualPath].transform.position + posY) {
                actualPath++;
                if (actualPath == path.Count - 1 && !townAttacked)
                    AttackTown();
                else
                    model.transform.LookAt(path[actualPath].transform.position + posY);
            }
        }
    }

    void AttackTown() {
        townAttacked = true;
        if (town != null)
            town.ReceiveDamage(damage * 2f);

        if (Dead != null)
            Dead(this);
        Destroy(this.gameObject);
    }
    protected virtual void Die() {
        DeathAnimation();
        attackBuilds = false;
        attacking = false;

        if (cheeseCoin != null)
            Instantiate(cheeseCoin, transform.position + transform.right, cheeseCoin.transform.rotation);

        if (Dead != null)
            Dead(this);
        Destroy(this.gameObject, 1.0f);
    }
    public void ReceiveDamage(float d) {
        health -= d;
        if (!hitted)
            StartCoroutine(HittedEffect());
        if (health <= 0)
            Die();
    }
    public void Heal(float h) {
        health += h;
        if (health >= maxHealth)
            health = maxHealth;
    }
    IEnumerator HittedEffect() {
        hitted = true;
        float timeWithHittedcolor = 0.15f;
        rend.material.color = hittedColor;
        yield return new WaitForSeconds(timeWithHittedcolor);
        rend.material.color = normalColor;
        hitted = false;
        yield return null;
    }

    private void OnTriggerEnter(Collider other) {
        if (!attackBuilds)
            return;

        if (other.gameObject.CompareTag("Structure")) {
            structureToAttack = other.GetComponent<Structure>();
            if (!attackingStructure)
                StartCoroutine(AttackStructure());
            return;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!attackBuilds)
            return;
        if (other.gameObject.CompareTag("Structure")) {
            attackingStructure = false;
            structureToAttack = null;
        }
    }

    protected virtual IEnumerator AttackStructure() {
        attackingStructure = true;

        float t = 0;
        if (animator != null && structureToAttack != null)
            AttackAnimation();

        if (structureToAttack != null)
            while (t < timeToAttack && structureToAttack != null) {
                t += Time.deltaTime;
                yield return null;
            }

        if (structureToAttack != null)
            Attack();

        ResetAttack();
        yield return null;
    }

    void Attack() {
        AkSoundEngine.PostEvent("enemy_attack", this.gameObject);
        structureToAttack.HitStructure(damage);
    }

    protected void ResetAttack() {
        if (attackingStructure && structureToAttack != null)
            StartCoroutine(AttackStructure());
        else {
            bc.enabled = false;
            attackingStructure = false;
            bc.enabled = true;
            MovementAnimation();
        }
    }

    protected virtual void AttackAnimation() {
        animator.SetBool("Attack", true);
        animator.SetBool("Death", false);
        animator.SetBool("Move", false);
        animator.Play("Attack");
    }
    protected virtual void MovementAnimation() {
        animator.SetBool("Attack", false);
        animator.SetBool("Death", false);
        animator.SetBool("Move", true);
        animator.Play("Movement");
    }
    protected virtual void DeathAnimation() {
        animator.SetBool("Attack", false);
        animator.SetBool("Death", true);
        animator.SetBool("Move", false);
        animator.Play("Death");
    }

    public void SetTown(Town t) {
        town = t;
    }
    public void SetPath(List<Transform> p) {
        path = p;
    }
}

