using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelected : MonoBehaviour {
    [SerializeField] float rotationSpeed;

    private void Update() {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }
}
