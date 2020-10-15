using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public delegate void TutorialChangedPhase(int phase);
    public static event TutorialChangedPhase TutorialPhaseChanged;

    EnemyManager enemyManager;

    [SerializeField] int actualPhase;
    [SerializeField] int enemyKilled = 0;

    [SerializeField] ScenesManager sm;
    [SerializeField] GameObject[] objectsToUnable;
    private void Start() {
        BuildCreatorTutorial.ClickedBase += ClickedBase;
        UITutorial.BuildTutorialPressed += ClickedTurretUI;
        Enemy.Dead += KilledEnemy;
    }
    private void OnDisable() {
        BuildCreatorTutorial.ClickedBase -= ClickedBase;
        UITutorial.BuildTutorialPressed -= ClickedTurretUI;
        Enemy.Dead -= KilledEnemy;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return))
            if (actualPhase == 0 || actualPhase == 1 || actualPhase == 4) {
                actualPhase++;
                if (TutorialPhaseChanged != null)
                    TutorialPhaseChanged(actualPhase);
            }
        
    }

    void ClickedBase() {
        if(actualPhase == 2) {
            actualPhase++;
            if (TutorialPhaseChanged != null)
                TutorialPhaseChanged(actualPhase);
        }
    }

    void ClickedTurretUI() {
        if(actualPhase == 3) {
            actualPhase++;
            if (TutorialPhaseChanged != null)
                TutorialPhaseChanged(actualPhase);
        }
    }

    void KilledEnemy(Enemy e) {
        enemyKilled++;
        if (enemyKilled == 5 && actualPhase == 5) {
            actualPhase++;
            if (TutorialPhaseChanged != null)
                TutorialPhaseChanged(actualPhase);
            StartCoroutine(EndTutorial());
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
