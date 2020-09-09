using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    [SerializeField] GameObject[] spellsViewer;
    [SerializeField] SpellBase[] spells;
    [SerializeField] List<float> spellTimers;
    [SerializeField] List<bool> canUseSpell;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform spellsParent;
    int spellToUse = 0;

    public enum SpellsList {
        None,
        Freeze,
        Fury
    }

    Camera cam;
    private void Start() {
        cam = Camera.main;
        spellToUse = (int)SpellsList.None;
        spellTimers.Add(0);
        canUseSpell.Add(false);
        for (int i = 0; i < spells.Length; i++)
            if (spells[i] != null) {
                spellTimers.Add(spells[i].GetCooldown());
                canUseSpell.Add(true);
            }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            spellToUse = (int)SpellsList.None;
            ActivateViewerSelectedSpell(spellToUse);
        }

        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200)) {
            if (hit.transform.CompareTag("Road")) {
                if (Input.GetKeyDown(KeyCode.Alpha1) && canUseSpell[(int)SpellsList.Freeze]) {
                    spellToUse = (int)SpellsList.Freeze;
                    ActivateViewerSelectedSpell(spellToUse);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseSpell[(int)SpellsList.Fury]) {
                    spellToUse = (int)SpellsList.Fury;
                    ActivateViewerSelectedSpell(spellToUse);
                }
            }
            else {
                spellToUse = (int)SpellsList.None;
                ActivateViewerSelectedSpell(spellToUse);
            }

            if (spellToUse != (int)SpellsList.None) {
                Vector3 pos = Vector3.zero;

                if (!hit.transform.CompareTag("Road")) {
                    if (spells[spellToUse] != null)
                        if (spellsViewer[spellToUse].gameObject.activeSelf) {
                            spellsViewer[spellToUse].gameObject.SetActive(false);
                            spellToUse = (int)SpellsList.None;
                        }
                }
                else if (hit.transform.CompareTag("Road")) {
                    if (spellsViewer[spellToUse] != null) {
                        if (!spellsViewer[spellToUse].gameObject.activeSelf)
                            spellsViewer[spellToUse].gameObject.SetActive(true);
                        pos = new Vector3((int)hit.point.x, spellsViewer[spellToUse].transform.position.y, (int)hit.point.z);
                        spellsViewer[spellToUse].transform.position = pos;
                    }
                    if (Input.GetMouseButtonDown(0)) {
                        if (spells[spellToUse] != null) {
                            if (canUseSpell[spellToUse]) {
                                SpellBase sb = Instantiate(spells[spellToUse], pos, Quaternion.identity, spellsParent);
                                StartCoroutine(StartCooldown(spellToUse));
                                if (spellsViewer[spellToUse] != null)
                                    spellsViewer[spellToUse].gameObject.SetActive(false);
                                spellToUse = (int)SpellsList.None;
                            }
                        }
                    }
                }
            }
        }
    }


    void ActivateViewerSelectedSpell(int stu) {
        for (int i = 0; i < spellsViewer.Length; i++)
            if (spellsViewer[i].gameObject != null)
                spellsViewer[i].gameObject.SetActive(false);

        if (spellsViewer[stu].gameObject != null)
            spellsViewer[stu].gameObject.SetActive(true);
    }

    IEnumerator StartCooldown(int stu) {
        canUseSpell[stu] = false;
        yield return new WaitForSeconds(spellTimers[stu]);
        canUseSpell[stu] = true;
        yield return null;
    }
}