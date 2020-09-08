using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] protected Town town;
    [SerializeField] protected List<Build> builds;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected int maxDistanceToAttack;
    [SerializeField] protected int maxDistanceToSelect;
    [SerializeField] protected float timeToAttack;

    bool goingToTown;
    bool attacking=false;
    int index;

    public delegate void EnemyDead(Enemy e);
    public static event EnemyDead Dead;


    protected virtual void Start() {
        attacking = false;
        GameplayManager.endEnemyAttack += OnDie;
        BuildingCreator.ChangedBuilds += SetBuildsList;
        Build.DestroyedBuild += RemoveBuildInList;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        SetNewObjective();
        StopCoroutine(LateStart());
        yield return null;
    }

    private void OnDisable() {
        GameplayManager.endEnemyAttack -= OnDie;
        BuildingCreator.ChangedBuilds -= SetBuildsList;
        Build.DestroyedBuild -= RemoveBuildInList;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, agent.destination) < maxDistanceToAttack) {
            if (!attacking)
                StartCoroutine(AttackObjective());
        }
    }

    public void SetNewObjective() {
        float nearest = 999999;
        for (int i = 0; i < builds.Count; i++) {
            if (builds[i] != null) {
                if (Vector3.Distance(transform.position, builds[i].transform.position) < nearest) {
                    nearest = Vector3.Distance(transform.position, builds[i].transform.position);
                    index = i;
                }
            }
        }

        if (nearest > maxDistanceToSelect || builds[index] == null) {
            if (town != null) {
                if (nearest > Vector3.Distance(transform.position, town.transform.position)) {
                    agent.destination = town.transform.position;
                    goingToTown = true;
                    return;
                }
            }
        }

        if (builds[index] != null) {
            agent.destination = builds[index].transform.position;
            goingToTown = false;
        }
    }

    public void SetBuildsList(List<Build> b) {
        builds = b;
        SetNewObjective();
    }

    public void SetTown(Town t) {
        town = t;
        SetNewObjective();
    }
    void RemoveBuildInList(Build b) {
        int ind = builds.IndexOf(b);
        builds.Remove(b);
        if (ind == index)
            SetNewObjective();
    }
    public virtual void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            OnDie();
        }
    }

    protected void OnDie() {
        if (Dead != null)
            Dead(this);

        Destroy(this.gameObject);
    }

    protected IEnumerator AttackObjective() {
        attacking = true;
        yield return new WaitForSeconds(timeToAttack);
        if (goingToTown) {
   
            if (town != null)
                town.ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
        else if (builds[index] != null) {
            builds[index].HitBuild(damage);    
        }
        attacking = false;
        StopCoroutine(AttackObjective());
        yield return null;
    }
}
