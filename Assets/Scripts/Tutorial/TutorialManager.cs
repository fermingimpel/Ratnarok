using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public delegate void TutorialChangedPhase(int phase);
    public static event TutorialChangedPhase TutorialPhaseChanged;

    EnemyManager enemyManager;

    [SerializeField] int actualPhase;
    [SerializeField] int enemyKilled = 0;

    [SerializeField] LoaderManager sm;
    [SerializeField] GameObject[] objectsToUnable;
    int[] phasesEnter = new int[] { 0, 1, 2, 4, 5, 6, 8, 10, 11, 12, 13 };
    int maxPhases = 14;
    private void Start() {
        UITutorial.BuildTutorialPressed += ClickedTurretUI;
        UITutorial.ClickedRatary += ClickedRatary;
        Enemy.Dead += KilledEnemy;
    }
    private void OnDisable() {
        UITutorial.BuildTutorialPressed -= ClickedTurretUI;
        UITutorial.ClickedRatary -= ClickedRatary;
        Enemy.Dead -= KilledEnemy;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Return))
            for (int i = 0; i < phasesEnter.Length; i++)
                if (actualPhase == phasesEnter[i]) {
                    actualPhase++;
                    i = 999;
                    if (actualPhase < maxPhases) {
                        if (TutorialPhaseChanged != null)
                            TutorialPhaseChanged(actualPhase);
                    }
                    else {
                        StartCoroutine(EndTutorial());
                    }

                }
    }

    void ClickedTurretUI() {
        if(actualPhase == 3) {
            actualPhase++;
            if (TutorialPhaseChanged != null)
                TutorialPhaseChanged(actualPhase);
        }
    }
    void ClickedRatary() {
        if(actualPhase == 9) {
            actualPhase++;
            TutorialPhaseChanged(actualPhase);
        }
    }

    void KilledEnemy(Enemy e) {
        enemyKilled++;
        if (enemyKilled == 3 && actualPhase == 7) {
            actualPhase++;
            if (TutorialPhaseChanged != null)
                TutorialPhaseChanged(actualPhase);
        }
    }

    IEnumerator EndTutorial() {
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i < objectsToUnable.Length; i++)
            if (objectsToUnable[i] != null)
                objectsToUnable[i].SetActive(false);
        sm.LoadScene("LevelSelector");
        StopCoroutine(EndTutorial());
        yield return null;
    }

}
