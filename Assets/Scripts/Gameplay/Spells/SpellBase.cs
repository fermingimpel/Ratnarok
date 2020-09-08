using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase : MonoBehaviour {
    [SerializeField] float cooldown;
    [SerializeField] Rigidbody rb;
    [SerializeField] bool canUseSpell;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public float GetCooldown() {
        return cooldown;
    }

    public bool GetCanUseSpell() {
        return canUseSpell;
    }

    public void UseSpell() {
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown() {
        canUseSpell = false;
        yield return new WaitForSeconds(cooldown);
        canUseSpell = true;
        yield return null;
    }

}
