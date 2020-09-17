﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] float timeInAttack;
    [SerializeField] float timeInPrepare;

    public delegate void EndOfAttack();
    public static event EndOfAttack EndEnemyAttack;

    public delegate void StartOfAttack();
    public static event StartOfAttack StartEnemyAttack;

    public delegate void UpdateUIState(Stage s);
    public static event UpdateUIState UIStateUpdate;

    public delegate void UpdateHordeBar(float actualSecond, float time);
    public static event UpdateHordeBar UpdateBarHorde;

    public enum Stage {
        Preparing,
        Attack
    }

    void Start() {
        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Preparing);

        StartCoroutine(LateStart());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Gameplay");
        }
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(PreparePhase());
        yield return null;
    }

    IEnumerator PreparePhase() {
        if (EndEnemyAttack != null)
            EndEnemyAttack();

        if (UIStateUpdate != null)
            UIStateUpdate(Stage.Preparing);

        float t = 0;
        while (t < timeInPrepare) {
            t += Time.deltaTime;
            if (UpdateBarHorde != null)
                UpdateBarHorde(t, timeInPrepare);
            yield return null;
        }

        StopCoroutine(PreparePhase());
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

        StopCoroutine(AttackPhase());
        StartCoroutine(PreparePhase());
        yield return null;
    }
   
}
