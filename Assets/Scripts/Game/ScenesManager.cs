using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    public void LoadScene(string stl) {
        SceneManager.LoadScene(stl);
    }
    public void CloseGame() {
        Application.Quit();
    }
}
