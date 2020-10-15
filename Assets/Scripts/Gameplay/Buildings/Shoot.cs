using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected Vector3 direction;
    protected float minX = -6f;

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }
    public void SetDamage(int d) {
        damage = d;
    }
}
