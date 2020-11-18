using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Building{
    [SerializeField] Fire fire;
    [SerializeField] float timeAttacking;
    [SerializeField] ParticleSystem ps;
    protected override void StopDefend() {
        StopCoroutine(PrepareAttack());
        StopCoroutine(Attack());
        defending = false;
    }

    protected override void StartDefend() {
        defending = true;
        StartCoroutine(PrepareAttack());
        Debug.Log("QAAASD");
    }

    IEnumerator PrepareAttack() {
        Debug.Log("AAAAA");
        if (defending) {
            yield return new WaitForSeconds(preparationTime);
            StopCoroutine(PrepareAttack());
            StartCoroutine(Attack());
            yield return null;
        }
        yield return null;
    }

    IEnumerator Attack() {
        if (defending) {
            ps.Play();
            Debug.Log("XD");
            fire.gameObject.SetActive(true);
            fire.SetDamage(damage);
            yield return new WaitForSeconds(timeAttacking);

            fire.gameObject.SetActive(false);
            Debug.Log("BRUH");
            ps.Stop();
            StopCoroutine(Attack());
            StartCoroutine(PrepareAttack());
            yield return null;
        }
        yield return null;
    }


}
