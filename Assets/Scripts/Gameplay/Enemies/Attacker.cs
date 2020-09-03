using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Enemy {
    public override void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            base.OnDie();
        }
    }
    
    protected override void AttackTown() {
        town.ReceiveDamage(damage);
        Destroy(this.gameObject);
    }

    protected override void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Town":
                AttackTown();
                break;
            case "Fence":
                break;
            default:
                break;
        }
    }
}
