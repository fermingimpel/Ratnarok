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
        Town.TownDestroyed += TownDestroyed;
    }
    private void OnDisable() {
        Town.TownDestroyed -= TownDestroyed;
    }

    void Update() {
        if (!canMoveCamera || town == null)
            return;

        if (transform.position.y > maxY)
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        else if (transform.position.y < minY)
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);

        if (Input.GetKey(KeyCode.W) && transform.position.y < maxY)
            transform.position += Vector3.up * speedCameraPos * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) && transform.position.y > minY)
            transform.position += Vector3.down * speedCameraPos * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * speedCameraRot * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.down * speedCameraRot * Time.deltaTime);

        if (town != null)
            cam.transform.LookAt(town.transform.position);
    }

    public void LockCameraMovement() {
        canMoveCamera = false;
    }
    public void UnlockCameraMovement() {
        canMoveCamera = true;
    }
    void TownDestroyed() {
        town = null;
        LockCameraMovement();
    }
}
