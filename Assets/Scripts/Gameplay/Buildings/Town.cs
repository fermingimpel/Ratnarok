using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

    [SerializeField] int health;

    public delegate void HPChanged(int hp);
    public static event HPChanged ChangedHP;

    void Start() {
        if (ChangedHP != null)
            ChangedHP(health);
    }

    public void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            health = 0;
            Destroy(this.gameObject);
        }

        if (ChangedHP != null)
            ChangedHP(health);
       
    }
}
