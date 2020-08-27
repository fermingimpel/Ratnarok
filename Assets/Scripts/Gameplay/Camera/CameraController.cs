using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] float speedCamera;
    [SerializeField] float speedZoom;

    float minY = 3;
    float maxY = 25;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        float zoom = -Input.GetAxis("Mouse ScrollWheel");
        Vector3 movement = new Vector3(hor * speedCamera, zoom * speedZoom, ver * speedCamera);
        transform.position += movement * Time.deltaTime;
        if (transform.position.y < minY)
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        if(transform.position.y > maxY)
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
    }
}
