using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Building {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 upset;

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
        Bullet s = Instantiate(shoot, transform.position + upset, Quaternion.identity);
        s.SetDirection(lookPos + upset);
        s.SetDamage(damage);
        StartCoroutine(PrepareAttack());
    }


}
