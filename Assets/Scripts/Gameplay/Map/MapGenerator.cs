using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] GameObject mountain;
    [SerializeField] Transform mountainParent;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                if (hit.transform.tag == "Base") {
                    Vector3 pos = new Vector3((int)hit.point.x, 1f, (int)hit.point.z);
                    if (pos.x % 2 != 0)
                        pos = new Vector3(pos.x + 1, pos.y, pos.z);
                    if (pos.z % 2 != 0)
                        pos = new Vector3(pos.x, pos.y, pos.z + 1);

                    GameObject go = Instantiate(mountain, pos, Quaternion.identity, mountainParent);
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                if (hit.transform.tag == "Mountain") {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
