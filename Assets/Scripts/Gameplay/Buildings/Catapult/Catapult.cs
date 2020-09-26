using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Build {

    [SerializeField] Shoot shoot;
    [SerializeField] Vector3 offset;

    private void Start() {
        StartCoroutine(PrepareAttack());
    }

    protected override void StopDefend() {
        StopCoroutine(PrepareAttack());
    }

    protected override void StartDefend() {
        StartCoroutine(PrepareAttack());
    }

    IEnumerator PrepareAttack() {
        yield return new WaitForSeconds(preparationTime);
        StopCoroutine(PrepareAttack());
        Attack();
        yield return null;
    }
    protected override void Attack() {
        Shoot s = Instantiate(shoot, transform.position + offset, shoot.transform.rotation);
        s.SetDirection(lookPos + offset);
        StartCoroutine(PrepareAttack());
    }
}
