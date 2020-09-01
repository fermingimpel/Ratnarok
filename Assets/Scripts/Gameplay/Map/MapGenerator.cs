using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] GameObject tile;
    [SerializeField] Material[] mats;
    [SerializeField] string[] tags;
    [SerializeField] string[] tileName;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] Transform[] parents;
    int tileSelected = 0;
    void Start() {
       //for (int i = 0; i < width; i++)
       //    for (int j = 0; j < height; j++) {
       //        GameObject go = Instantiate(tile, new Vector3( (-width) + i * 2, 0, (-height) +  j * 2), tile.transform.rotation, parents[0]);
       //        go.tag = tags[0];
       //        go.name = tileName[0];
       //        go.GetComponent<Renderer>().material = mats[0];
       //    }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            tileSelected = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            tileSelected = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            tileSelected = 2;
        
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                hit.transform.gameObject.GetComponent<Renderer>().material = mats[tileSelected];
                hit.transform.tag = tags[tileSelected];
                hit.transform.name = tileName[tileSelected];
                hit.transform.parent = parents[tileSelected];
            }
        }
    }  
}
