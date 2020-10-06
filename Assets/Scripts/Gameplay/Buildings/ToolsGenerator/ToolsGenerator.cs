using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsGenerator : Building {
    [SerializeField] BuildingCreator building;
    [SerializeField] int toolsToAdd;
    private void Start() {
        building = FindObjectOfType<BuildingCreator>();
        StartCoroutine(GenerateTools());
    }
  
    IEnumerator GenerateTools() {
        yield return new WaitForSeconds(preparationTime);
        StopCoroutine(GenerateTools());
        AddTools();
        yield return null;
    }

    void AddTools() {
        building.AddTools(toolsToAdd);
        StartCoroutine(GenerateTools());
    }
}
