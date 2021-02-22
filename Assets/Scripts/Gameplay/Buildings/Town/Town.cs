using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {
    [SerializeField] float initialHealth;
    [SerializeField] float health;

    public delegate void HPChanged(float hp, float maxHP);
    public static event HPChanged ChangedHP;

    public static Action TownDestroyed;

    private void Start() {
        health = initialHealth;
    }

    public void ReceiveDamage(float d) {
        health -= d;
        if (ChangedHP != null)
            ChangedHP(health, initialHealth);
        AkSoundEngine.PostEvent("Hit_Cheese", this.gameObject);
        if(health <= 0) {
            health = 0;

            if (TownDestroyed != null)
                TownDestroyed();

            Destroy(this.gameObject);
        }
    }
}
