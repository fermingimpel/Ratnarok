using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField] Town town;
    [SerializeField] NavMeshAgent agent;
    void Start() {
        town = FindObjectOfType<Town>();
        if (town != null)
            agent.destination = town.transform.position;
    }
}
