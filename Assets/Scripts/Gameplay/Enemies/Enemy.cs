using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] Town town;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int health;
    void Start() {
        town = FindObjectOfType<Town>();
        if (town != null)
            agent.destination = town.transform.position;
    }

    public void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
