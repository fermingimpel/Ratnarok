using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] GameObject tile;
    [SerializeField] Transform parent;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] Vector3 offset;

    void Start() {
        for(int i=0;i<width;i++)
            for(int j = 0; j < height; j++) {
                GameObject go = Instantiate(tile, new Vector3(i * offset.x, offset.y, j * offset.z), tile.transform.rotation, parent);
            }

    }

}
