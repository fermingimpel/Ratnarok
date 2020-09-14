using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAlly : MonoBehaviour {
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Vector3 initialPos;
    [SerializeField] float distanceToAttack;
    [SerializeField] float distanceToGoingToTarget;
    [SerializeField] float timeToAttack;
    [SerializeField] int damage;
    [SerializeField] float speed;

    int enemyIndex = 0;

    public enum State {
        None,
        GoingToAttack,
        GoingToInitialPos,
        Attacking
    }

    State unitState;

    void Start() {
        unitState = State.None;
        initialPos = transform.position;
    }

    private void Update() {
        if (unitState == State.None)
            Debug.Log("Coco");
        //if (CheckIfEnemyIsNear()) {
        //    StopAllCoroutines();
        //    StartCoroutine(GoingToAttack());
        //}

    }

    IEnumerator GoingToAttack() {
        unitState = State.GoingToAttack;
        if (enemies[enemyIndex] != null) {
            while (Vector3.Distance(transform.position, enemies[enemyIndex].transform.position) < distanceToAttack) {
                transform.position = Vector3.MoveTowards(transform.position, enemies[enemyIndex].transform.position, speed * Time.deltaTime);
                yield return null;
            }
            StartCoroutine(Attack(enemyIndex));
        }
        StopCoroutine(GoingToAttack());
        yield return null;
    }

    IEnumerator Attack(int ind) {
        unitState = State.Attacking;
        yield return new WaitForSeconds(timeToAttack);
        if (enemies[ind] != null)
            enemies[ind].ReceiveDamage(damage);
        StopCoroutine(Attack(ind));
        if (CheckIfEnemyIsNear())
            StartCoroutine(GoingToAttack());
        else
            StartCoroutine(GoToOriginalPos());
        yield return null;
    }

    IEnumerator GoToOriginalPos() {
        unitState = State.GoingToInitialPos;
        bool goToOrigin = true;
        while (goToOrigin) {
            transform.position = Vector3.MoveTowards(transform.position, initialPos, speed * Time.deltaTime);
            if (transform.position == initialPos) {
                goToOrigin = false;
            }

            yield return null;
        }
        unitState = State.None;
        StopCoroutine(GoToOriginalPos());
    }
    bool CheckIfEnemyIsNear() {
        //bool CheckIfEnemyIsNear() {
        float nearest = 999999;
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] != null) {
                if (Vector3.Distance(transform.position, enemies[i].transform.position) < nearest) {
                    nearest = Vector3.Distance(transform.position, enemies[i].transform.position);
                    enemyIndex = i;
                }
            }
        }

        if (nearest < distanceToGoingToTarget)
            return true;

        return false;
    }

    public void SetEnemyList(List<Enemy> e) {
        enemies = e;
    }
}
