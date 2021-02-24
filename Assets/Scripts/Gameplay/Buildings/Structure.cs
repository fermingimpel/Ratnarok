using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : MonoBehaviour {
    [SerializeField] protected float cheeseCost;
    [SerializeField] protected float attackPreparationTime;
    public float cooldownToCreate;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] public bool defending = false;
    float maxHealth;

    public delegate void StructureDestroyed(Structure s);
    public static event StructureDestroyed DestroyedStructure;

    [SerializeField] List<Transform> path;
    [SerializeField] protected Vector3 lookPos;

    [SerializeField] List<Renderer> rends;
    bool hitted = false;
    [SerializeField] Color hittedColor;
    [SerializeField] Color normalColor;
    [SerializeField] float timeHittedColor;

    [SerializeField] GameObject canvasStructure;
    [SerializeField] Image[] hpBars;

    [SerializeField] protected Animator animator;
    [SerializeField] protected Animator ratAnimator;

    protected void Start() {
        maxHealth = health;
    }
    protected virtual void FixedUpdate() {
        for (int i = 0; i < hpBars.Length; i++)
            if (hpBars[i] != null)
                hpBars[i].transform.LookAt(Camera.main.transform);
    }
    protected virtual void Attack() {
        Debug.Log("Attack");
    }

    public void HitStructure(float d) {
        health -= d;
        if(health > 0) {
            if (hpBars[0] != null)
                hpBars[0].fillAmount = health / maxHealth;
            if (!hitted)
                StartCoroutine(HitEffect());
            return;
        }
        if (DestroyedStructure != null)
            DestroyedStructure(this);

        Destroy(this.gameObject);
    }

    IEnumerator HitEffect() {
        hitted = true;
        for (int i = 0; i < rends.Count; i++)
            if (rends[i] != null)
                rends[i].material.color = hittedColor;
        yield return new WaitForSeconds(timeHittedColor);
        for (int i = 0; i < rends.Count; i++)
            if (rends[i] != null)
                rends[i].material.color = normalColor;
        hitted = false;
        yield return null;
    }
    public void SetPath(List<Transform> p) {
        path = p;
    }
    public void SetLookAt(int la) {
        if (path[la] != null) {
            transform.LookAt(new Vector3(path[la].position.x, transform.position.y, path[la].position.z));
            lookPos = new Vector3(path[la].position.x, transform.position.y, path[la].position.z);
        }
    }
    public void SetDefending(bool d) {
        defending = d;
    }
    public float GetCheeseCost() {
        return cheeseCost;
    }
    public float GetCooldownToCreate() {
        return cooldownToCreate;
    }
    public float GetHP() {
        return health;
    }
    public float GetMaxHP() {
        return maxHealth;
    }
}
