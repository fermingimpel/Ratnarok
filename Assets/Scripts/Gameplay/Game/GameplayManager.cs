using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    [SerializeField] float timeInAttack;
    [SerializeField] float timeInFirstPrepare;

    public delegate void EndOfAttack();
    public static event EndOfAttack EndEnemyAttack;

    public delegate void StartOfAttack();
    public static event StartOfAttack StartEnemyAttack;

    public delegate void StartPreAttack();
    public static event StartPreAttack StartPreAtk;

    public delegate void UpdateUIState(Stage s);
    public static event UpdateUIState UIStateUpdate;

    public delegate void StarHordeAttack(int horde);
    public static event StarHordeAttack StartAttackHorde;

    [SerializeField] GameObject winScreenUI;
    [SerializeField] GameObject loseScreenUI;

    bool attackPhase = false;

    [SerializeField] CameraController cc;
    [SerializeField] EnemyManager em;
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
        attackPhase = false;
        StopCoroutine(AttackPhase());
        winScreenUI.SetActive(true);
        cc.enabled = false;
    }

    void LoseGame() {
        attackPhase = false;
        StopCoroutine(AttackPhase());
        loseScreenUI.SetActive(true);
        cc.enabled = false;
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

        yield return new WaitForSeconds(timeInFirstPrepare - 2.0f);
        if (StartPreAtk != null)
            StartPreAtk();
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(AttackPhase());
        yield return null;
    }

    IEnumerator AttackPhase() {
        attackPhase = true;
        if (StartEnemyAttack != null)
            StartEnemyAttack();

        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Attack);

        while (!em.GetAllHordesCompleted()) {
            yield return new WaitForSeconds(0.5f);
        }

        if (EndEnemyAttack != null)
            EndEnemyAttack();
        WinGame();

        yield return new WaitForSeconds(3.0f);
    }

    public void StartHorde() {
        if (StartAttackHorde != null)
            StartAttackHorde(0);
    }
    public void StopTimeCount() {
        timeInAttack = 999999;
    }

}
