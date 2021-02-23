using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberRat : Enemy {
    [SerializeField] Rigidbody rb;
    [SerializeField] List<GameObject> balloons;
    [SerializeField] GameObject bomb;
    [SerializeField] Rigidbody rbBomb;

    [SerializeField] Vector3 initialBombPos;
    protected override void AttackAnimation() {
        base.AttackAnimation();
        bomb.transform.localPosition = initialBombPos;
        rbBomb.velocity = Vector3.zero;
        rbBomb.angularVelocity = Vector3.zero;
        rbBomb.useGravity = true;
    }
    protected override void MovementAnimation() {
        base.MovementAnimation();
        bomb.transform.localPosition = initialBombPos;
        rbBomb.velocity = Vector3.zero;
        rbBomb.angularVelocity = Vector3.zero;
        rbBomb.useGravity = false;
    }
    protected override void Die() {
        base.Die();
        for (int i = 0; i < balloons.Count; i++)
            balloons[i].SetActive(false);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }
}
