using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {
   [SerializeField] GameObject spawnerMap;
    [SerializeField] Transform spawnerParent;
    [SerializeField] int width;
    [SerializeField] int height;
    void Start() {
        for (int i = -1; i <= width; i++)
            for (int j = -1; j <= height; j++) {
                if (i == -1 || i == width || j == -1 || j == height) {
                    GameObject go = Instantiate(spawnerMap, new Vector3(i, 0, j), spawnerMap.transform.rotation, spawnerParent);
                }
            }
    }

    // Update is called once per frame
    void Update() {

    }
}
