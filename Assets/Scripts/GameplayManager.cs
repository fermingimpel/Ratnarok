using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] float timeInAttack;
    [SerializeField] float timeInPrepare;

    public delegate void EndOfAttack();
    public static event EndOfAttack endEnemyAttack;

    public delegate void StartOfAttack();
    public static event StartOfAttack startEnemyAttack;
    void Start()
    {
        StartCoroutine(LateStart());   
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(0.05f);
        if (endEnemyAttack != null)
            endEnemyAttack();
        StartCoroutine(PreparePhase());
        StopCoroutine(LateStart());
        yield return null;
    }

    IEnumerator AttackPhase() {
        yield return new WaitForSeconds(timeInAttack);
        StopCoroutine(AttackPhase());
        StartCoroutine(PreparePhase());
        if (endEnemyAttack != null)
            endEnemyAttack(); 
        yield return null;
    }
    IEnumerator PreparePhase() {
        yield return new WaitForSeconds(timeInPrepare);
        StopCoroutine(PreparePhase());
        StartCoroutine(AttackPhase());
        if (startEnemyAttack != null)
            startEnemyAttack();
        yield return null;
    }
}
