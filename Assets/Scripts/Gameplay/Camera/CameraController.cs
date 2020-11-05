using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] float speedCameraRot;
    [SerializeField] float speedCameraPos;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject town;

    [SerializeField] float minY;
    [SerializeField] float maxY;

    bool canMoveCamera = true;

    private void Start() {
        BuildingCreator.ClickedBase += ClickedBase;
        UIBuildingsDisc.UIButtonPressed += UnClickedBase;
    }
    private void OnDisable() {
        BuildingCreator.ClickedBase -= ClickedBase;
        UIBuildingsDisc.UIButtonPressed += UnClickedBase;
    }

    void Update() {
        if (!canMoveCamera)
            return;

        if (Input.GetKey(KeyCode.W) && transform.position.y < maxY)
            transform.position += Vector3.up * speedCameraPos * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) && transform.position.y > minY)
            transform.position += Vector3.down * speedCameraPos * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) 
            transform.Rotate(Vector3.up * speedCameraRot * Time.deltaTime); 
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.down * speedCameraRot * Time.deltaTime);

        cam.transform.LookAt(town.transform.position);
    }

    void ClickedBase() {
        canMoveCamera = false;
    }
    void UnClickedBase() {
        canMoveCamera = true;
    }

}
