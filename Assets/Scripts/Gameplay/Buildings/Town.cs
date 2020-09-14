using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

    [SerializeField] int maxHealth;
    [SerializeField] int health;
    
    public delegate void HPChanged(float hp, float maxHp);
    public static event HPChanged ChangedHP;

    public delegate void TownDestroyed();
    public static event TownDestroyed DestroyedTown;

    void Start() {
        health = maxHealth;
        if (ChangedHP != null)
            ChangedHP((float)health, (float)maxHealth);
    }

    public void ReceiveDamage(int d) {
        if (this != null) {
            health -= d;
            if (health <= 0) {
                health = 0;
                if (DestroyedTown != null)
                    DestroyedTown();
                Destroy(this.gameObject);
            }

            if (ChangedHP != null)
                ChangedHP((float)health, (float)maxHealth);
        }
    }
}
