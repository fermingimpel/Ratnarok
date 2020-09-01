using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] protected List<Enemy> enemies;
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {

    }


    public virtual void SetEnemyList(List<Enemy> list) {
        enemies = list;
    }
}
