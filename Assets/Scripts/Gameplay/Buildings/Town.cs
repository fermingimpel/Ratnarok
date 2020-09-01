using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

    [SerializeField] int health;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ReceiveDamage(int d) {
        health -= d;
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
