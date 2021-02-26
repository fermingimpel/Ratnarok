using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Enemy {

    [SerializeField] float timeToHeal;
    [SerializeField] float heal;
    [SerializeField] float rangeToHeal;
    float timertoHeal = 0;
    [SerializeField] List<Enemy> allies;

    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject healBase;

    protected virtual void Update() {
        base.Update();

        healBase.transform.localPosition = new Vector3(model.transform.localPosition.x, -0.4f, model.transform.localPosition.z);

        timertoHeal += Time.deltaTime;
        if (timertoHeal >= timeToHeal) {
            allies.Clear();
            allies = new List<Enemy>(FindObjectsOfType<Enemy>());
            timertoHeal = 0;
            HealAllies();
        }

        if(!ps.isPlaying)
            healBase.SetActive(false);
    }

    void HealAllies() {
        healBase.SetActive(true);
        ps.Play();
        for (int i = 0; i < allies.Count; i++)
            if (allies[i] != null)
                if (Vector3.Distance(transform.position, allies[i].transform.position) <= rangeToHeal) 
                    allies[i].Heal(heal);
    }
}
