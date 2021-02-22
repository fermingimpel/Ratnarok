using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureCreator : MonoBehaviour {
    [SerializeField] Structure[] structures;
    [SerializeField] List<float> cooldownToCreateStructures;
    [SerializeField] List<bool> canCreateStructure;
    [SerializeField] List<float> actualCooldownsToCreate;
    [SerializeField] List<float> cheeseCosts;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform structuresParent;
    [SerializeField] List<Structure> structuresCreated;
    [SerializeField] bool defending = false;
    public int maxStructures;

    [Serializable]
    public class Paths {
        public List<Transform> pos;
    }
    [SerializeField] List<Paths> paths;

    public enum TypeOfStructure {
        Canonn,
        Fence,
        Catapult,
        Flamethrower,
        Crossbow
    }

    private void Start() {
        structuresCreated = new List<Structure>();
        structuresCreated.Clear();

        for(int i=0;i<structures.Length;i++) {
            cooldownToCreateStructures.Add(structures[i].GetCooldownToCreate());
            cheeseCosts.Add(structures[i].GetCheeseCost());
            canCreateStructure.Add(true);
            actualCooldownsToCreate.Add(0.0f);
        }
        maxStructures = structures.Length;

        Structure.DestroyedStructure += DestroyedStructure;
    }
    private void OnDisable() {
        Structure.DestroyedStructure -= DestroyedStructure;
    }

    private void Update() {
        for(int i = 0; i < structures.Length; i++) {
            if (!canCreateStructure[i]) {
                actualCooldownsToCreate[i] += Time.deltaTime;
                if (actualCooldownsToCreate[i] >= cooldownToCreateStructures[i]) {
                    actualCooldownsToCreate[i] = 0.0f;
                    canCreateStructure[i] = true;
                }
            }
        }
    }

    public void CreateStructure(TypeOfStructure tos, Vector3 pos, Tile t) {
        AkSoundEngine.PostEvent("click_torret_construction", this.gameObject);
        Structure s = Instantiate(structures[(int)tos], pos, structures[(int)tos].transform.rotation, structuresParent);
        s.SetDefending(defending);
        s.SetPath(paths[t.GetPathIndex()].pos);
        s.SetLookAt(t.GetLookAt());
        canCreateStructure[(int)tos] = false;
        structuresCreated.Add(s);
    }
    public void DestroyedStructure(Structure s) {
        structuresCreated.Remove(s);
        AkSoundEngine.PostEvent("torret_destruction", this.gameObject);
    }
    public void StartDefending() {
        defending = true;
        for (int i = 0; i < structuresCreated.Count; i++)
            if (structuresCreated[i] != null)
                structuresCreated[i].defending = true;
    }
    public void StopDefending() {
        defending = false;
        for (int i = 0; i < structuresCreated.Count; i++)
            if (structuresCreated[i] != null)
                structuresCreated[i].defending = false;
    }
    public float GetCheeseCost(TypeOfStructure tos) {
        return cheeseCosts[(int)tos];
    }
    public bool GetCanCreateStructure(TypeOfStructure tos) {
        return canCreateStructure[(int)tos];
    }
}
