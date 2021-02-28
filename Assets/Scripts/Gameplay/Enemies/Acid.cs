using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Enemy {
    [SerializeField] float timeAcidOn;
    [SerializeField] AcidSpread acid;
    protected override void Update() {
        base.Update();
    }
    protected override IEnumerator AttackStructure() {
        attackingStructure = true;

        int actualTics = 0;
        if (structureToAttack != null) {
            acid.gameObject.SetActive(true);
            while (actualTics < acid.maxHits && structureToAttack != null) {
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

        ResetAttack();
        yield return null;
    }
}
