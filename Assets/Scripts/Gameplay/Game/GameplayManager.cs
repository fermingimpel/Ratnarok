using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] float timeInAttack;
    [SerializeField] float timeInFirstPrepare;

    public delegate void EndOfAttack();
    public static event EndOfAttack EndEnemyAttack;

    public delegate void StartOfAttack();
    public static event StartOfAttack StartEnemyAttack;

    public delegate void UpdateUIState(Stage s);
    public static event UpdateUIState UIStateUpdate;

    public delegate void UpdateHordeBar(float actualSecond, float time);
    public static event UpdateHordeBar UpdateBarHorde;

    [SerializeField] GameObject winScreenUI;
    [SerializeField] GameObject loseScreenUI;

    public enum Stage {
        Preparing,
        Attack
    }

    void Start() {
        winScreenUI.SetActive(false);
        loseScreenUI.SetActive(false);

        Time.timeScale = 1.0f;
        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Preparing);

        StartCoroutine(LateStart());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Gameplay");
        }
    }

    void WinGame() {
        winScreenUI.SetActive(true);
    }
       
    void LoseGame() {
        loseScreenUI.SetActive(true);
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(FirstPreparePhase());
        yield return null;
    }

    IEnumerator FirstPreparePhase() {
        if (EndEnemyAttack != null)
            EndEnemyAttack();

        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Preparing);

        float t = 0;
        while (t < timeInFirstPrepare) {
            t += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(FirstPreparePhase());
        StartCoroutine(AttackPhase());
        yield return null;
    }

    IEnumerator AttackPhase() {
        if (StartEnemyAttack != null)
            StartEnemyAttack();

        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Attack);

        float t = 0;
        while(t < timeInAttack) {
            t += Time.deltaTime;
            if (UpdateBarHorde != null)
                UpdateBarHorde(t, timeInAttack);
            yield return null;
        }

        if (EndEnemyAttack != null)
            EndEnemyAttack();
        StopCoroutine(AttackPhase());
        WinGame();
        yield return null;
    }
   
}
