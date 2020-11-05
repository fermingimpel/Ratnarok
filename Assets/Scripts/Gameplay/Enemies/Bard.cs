using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : Enemy {
    [SerializeField] float timeToHeal;
    [SerializeField] float heal;
    [SerializeField] float rangeToHeal;


    [SerializeField] List<Enemy> allies;
    [SerializeField] EnemyManager em;
    protected virtual void Start() {
        em = FindObjectOfType<EnemyManager>();
        base.Start();
        StartCoroutine(PrepareHeal());
    }

    // Update is called once per frame
    protected virtual void Update() {
        base.Update();
    }

    public void SetHeal(float h) {
        heal = h;
    }
    public float GetHeal() {
        return heal;
    }

    IEnumerator PrepareHeal() {
        yield return new WaitForSeconds(timeToHeal);
        HealAllies();

        StopCoroutine(PrepareHeal());
        yield return null;
    }

    void HealAllies() {
        allies = em.GetEnemies();


        for (int i = 0; i < allies.Count; i++)
            if (allies[i] != null)
                if (Vector3.Distance(transform.position, allies[i].transform.position) < rangeToHeal)
                    allies[i].Heal(heal);


        StartCoroutine(PrepareHeal());
    }


}
