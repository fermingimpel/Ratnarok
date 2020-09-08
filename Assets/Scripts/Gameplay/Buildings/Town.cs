using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

    [SerializeField] int health;

    public delegate void HPChanged(int hp);
    public static event HPChanged ChangedHP;

    public delegate void TownDestroyed();
    public static event TownDestroyed DestroyedTown;

    void Start() {
        if (ChangedHP != null)
            ChangedHP(health);
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
                ChangedHP(health);
        }
    }
}
