using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Enemy {
    [SerializeField] float timeAcidOn;
    [SerializeField] AcidSpread acid;
    [SerializeField] ParticleSystem ps;
    protected override void Update() {
        base.Update();
    }
    protected override IEnumerator AttackStructure() {
        attackingStructure = true;

        int actualTics = 0;
        if (structureToAttack != null) {
            acid.gameObject.SetActive(true);
            while (actualTics < acid.maxHits && structureToAttack != null && health > 0) {
                ps.Play();
                if (animator != null && structureToAttack != null)
                    AttackAnimation();
                if (structureToAttack != null)
                    yield return new WaitForSeconds(acid.timeBetweenHits);
                AkSoundEngine.PostEvent("torret_stuff", this.gameObject);
                actualTics++;
            }
            //yield return new WaitForSeconds(timeAcidOn);
            acid.gameObject.SetActive(false);
        }
        if (structureToAttack != null)
            yield return new WaitForSeconds(timeToAttack);
        if(health > 0)
         ResetAttack();
        yield return null;
    }
}
