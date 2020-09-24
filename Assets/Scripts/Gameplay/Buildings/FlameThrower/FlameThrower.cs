using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Build
{
    [SerializeField] GameObject fire;
    [SerializeField] float timeAttacking;
    protected override void StopDefend() {
        StopCoroutine(PrepareAttack());
    }

    protected override void StartDefend() {
        StartCoroutine(PrepareAttack());
    }

    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(preparationTime);
        StopCoroutine(PrepareAttack());
        StartCoroutine(Attack());
        yield return null;
    }

    IEnumerator Attack() {
        fire.SetActive(true);
        yield return new WaitForSeconds(timeAttacking);

        fire.SetActive(false);
        StopCoroutine(Attack());
        StartCoroutine(PrepareAttack());
        yield return null;
    }


}
