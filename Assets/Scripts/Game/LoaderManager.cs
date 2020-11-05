using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour {
    [SerializeField] GameObject loadScreen;

    public void LoadScene(string stl) {
        Time.timeScale = 1.0f;
        StartCoroutine(Load(stl));
    }

    IEnumerator Load(string stl) {
        loadScreen.gameObject.SetActive(true);

        SceneManager.LoadScene(stl);

        while (!stl.Equals(SceneManager.GetActiveScene().name) ) {
            yield return null;
        }

        loadScreen.gameObject.SetActive(false);
        yield return null;
    }

    public void CloseGame() {
        Application.Quit();
    }
}
