using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelected : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private void Start() {
        BuildingCreator.BSelected += ClickedBase;
        UIBuildingsDisc.UIButtonPressed += UnSelectedBase;
        this.gameObject.SetActive(false);
    }
    private void OnDestroy() {
        BuildingCreator.BSelected -= ClickedBase;
        UIBuildingsDisc.UIButtonPressed -= UnSelectedBase;
    }
    void Update() {
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    void ClickedBase(Vector3 pos) {
        this.gameObject.SetActive(true);
        transform.localPosition = new Vector3(pos.x, 0.11f, pos.z);
    }
    void UnSelectedBase() {
        this.gameObject.SetActive(false);
    }

}
