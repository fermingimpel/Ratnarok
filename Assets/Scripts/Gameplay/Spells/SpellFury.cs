using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFury : SpellBase {
    [SerializeField] float timeOfFury;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Build") {
            Build b = other.gameObject.GetComponent<Build>();
            if (b != null)
                StartCoroutine(StartFury(b));
        }
    }

    IEnumerator StartFury(Build b) {
        if (b != null)
            b.SetFury(true);
        yield return new WaitForSeconds(timeOfFury);
        if (b != null)
            b.SetFury(false);
        yield return null;
    }

    //IEnumerator 

}
