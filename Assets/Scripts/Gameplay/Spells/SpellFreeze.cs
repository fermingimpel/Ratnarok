using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFreeze : SpellBase {
    [SerializeField] float freezeTime;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            if (e != null)
                StartCoroutine(Freeze(e));
        }
    }

    IEnumerator Freeze(Enemy e) {
        if (e != null)
            e.SetFreeze(true);
        yield return new WaitForSeconds(freezeTime);
        if (e != null)
            e.SetFreeze(false);
        yield return null;
    }

}
