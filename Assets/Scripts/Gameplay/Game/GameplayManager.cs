using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    [SerializeField] float timeInFirstPrepare;

    public delegate void EndOfAttack();
    public static event EndOfAttack EndEnemyAttack;

    public delegate void StartOfAttack();
    public static event StartOfAttack StartEnemyAttack;

    public delegate void StartPreAttack();
    public static event StartPreAttack StartPreAtk;

    public delegate void UpdateUIState(Stage s);
    public static event UpdateUIState UIStateUpdate;

    [SerializeField] GameObject winScreenUI;
    [SerializeField] GameObject loseScreenUI;

    [SerializeField] CameraController cc;
    [SerializeField] EnemyManager em;
    [SerializeField] LoaderManager lm;

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

        Town.DestroyedTown += LoseGame;
    }

    private void OnDisable() {
        Town.DestroyedTown -= LoseGame;
    }
    void WinGame() {
        if (!lm.GetChangingLevel()) {
            winScreenUI.SetActive(true);
            cc.enabled = false;
        }
    }

    void LoseGame() {
        if (!lm.GetChangingLevel()) {
            loseScreenUI.SetActive(true);
            cc.enabled = false;
        }
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(FirstPreparePhase());
        yield return null;
    }

    IEnumerator FirstPreparePhase() {
        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Preparing);

        yield return new WaitForSeconds(timeInFirstPrepare - 2.0f);
        if (StartPreAtk != null)
            StartPreAtk();
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(AttackPhase());
        yield return null;
    }

    IEnumerator AttackPhase() {
        if (StartEnemyAttack != null)
            StartEnemyAttack();

        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Attack);

        while (!em.GetAllHordesCompleted()) {
            yield return new WaitForSeconds(0.5f);
        }

        if (EndEnemyAttack != null)
            EndEnemyAttack();

        yield return new WaitForSeconds(3.0f);
       
        WinGame();
    }
}
