using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour {
    [SerializeField] GameObject loadScreen;
    bool changingLevel = false;
    private void Start() {
        changingLevel = false;
    }
    public void LoadScene(string stl) {

        Time.timeScale = 1.0f;
        StartCoroutine(Load(stl));
    }

    IEnumerator Load(string stl) {
        if (!changingLevel) {
            changingLevel = true;
            AkSoundEngine.StopAll();
            loadScreen.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(stl);
            while (!stl.Equals(SceneManager.GetActiveScene().name)) {
                yield return null;
            }

            loadScreen.gameObject.SetActive(false);
            yield return null;
        }
        yield return null;
    }
    public bool GetChangingLevel() {
        return changingLevel;
    }
    public void CloseGame() {
        Application.Quit();
    }
}
