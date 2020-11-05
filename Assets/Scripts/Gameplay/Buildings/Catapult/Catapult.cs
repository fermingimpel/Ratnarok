using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Building {
    [SerializeField] Bullet shoot;
    [SerializeField] Vector3 offset;
    protected override void Start() {
        base.Start();
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
        Bullet s = Instantiate(shoot, transform.position + offset, shoot.transform.rotation);
        s.SetDirection(transform.forward + offset);
        s.SetDamage(damage);
        StartCoroutine(PrepareAttack());
    }
}
