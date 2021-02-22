using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Enemy {

    [SerializeField] float timeToHeal;
    [SerializeField] float heal;
    [SerializeField] float rangeToHeal;
    float timertoHeal = 0;
    [SerializeField] List<Enemy> allies;

    protected virtual void Update() {
        base.Update();

        timertoHeal += Time.deltaTime;
        if (timertoHeal >= timeToHeal) {
            allies.Clear();
            allies = new List<Enemy>(FindObjectsOfType<Enemy>());
            timertoHeal = 0;
            HealAllies();
        }
    }

    void HealAllies() {
        for (int i = 0; i < allies.Count; i++)
            if (allies[i] != null)
                if (Vector3.Distance(transform.position, allies[i].transform.position) <= rangeToHeal)
                    allies[i].Heal(heal);

    }
}
