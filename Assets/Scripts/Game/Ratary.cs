using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratary : MonoBehaviour {
    [SerializeField] List<GameObject> pages;
    [SerializeField] int actualPage = 0;

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            PreviousPage();
        else if (Input.GetKeyDown(KeyCode.D))
            NextPage();
        
    }
    public void PreviousPage() {
        if (actualPage > 0) {
            pages[actualPage].SetActive(false);
            actualPage--;
            pages[actualPage].SetActive(true);
        }
    }
    public void NextPage() {
        if (actualPage < pages.Count - 1) {
            pages[actualPage].SetActive(false);
            actualPage++;
            pages[actualPage].SetActive(true);
        }
    }
}
