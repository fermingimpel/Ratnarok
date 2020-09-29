using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    public void LoadScene(string stl) {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(stl);
    }
    public void CloseGame() {
        Application.Quit();
    }
}
