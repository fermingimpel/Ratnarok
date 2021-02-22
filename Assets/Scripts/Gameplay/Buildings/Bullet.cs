using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Vector3 direction;

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }
    public void SetDamage(float d) {
        damage = d;
    }
}
