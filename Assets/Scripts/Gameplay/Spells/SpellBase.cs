using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour {
    [SerializeField] float duration;
    [SerializeField] float cooldown;
    [SerializeField] bool canUseSpell;
    void Start() {
        Destroy(this.gameObject, duration);
    }

    public float GetCooldown() {
        return cooldown;
    }
}
