using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    [SerializeField] SpellBase[] spells;
    [SerializeField] Vector3 upset;
    [SerializeField] Transform spellParent;
    int spellToUse=0;

    public enum SpellsList {
        None,
        Freeze,
        Fury
    }

    Camera cam;
    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            spellToUse = (int)SpellsList.Freeze;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            spellToUse = (int)SpellsList.Fury;
        }


        if (Input.GetKeyDown(KeyCode.Space) && spellToUse != (int)SpellsList.None) {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 200)) {
                if(hit.transform.tag == "Road") {
                    if (spells[spellToUse] != null) {
                        Vector3 pos = new Vector3((int)hit.transform.position.x, hit.point.y + upset.y, (int)hit.transform.position.z);
                        SpellBase sb = Instantiate(spells[spellToUse], pos, Quaternion.identity, spellParent);
                        sb.UseSpell();
                    }
                }
            }
        }
    }
}
