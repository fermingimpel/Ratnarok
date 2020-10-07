using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberRat : Enemy {
    // Start is called before the first frame update
    [SerializeField] GameObject body;
    [SerializeField] float speedUpDown;
    protected override void Start() {
        base.Start();
        attackBuilds = false;
        StartCoroutine(MoveUp());
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    IEnumerator MoveUp() {
        Vector3 newPos = body.transform.localPosition + Vector3.up;
        while(body.transform.localPosition != newPos) {
            body.transform.localPosition = Vector3.MoveTowards(body.transform.localPosition, newPos, speedUpDown * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(MoveUp());
        StartCoroutine(MoveDown());
        yield return null;
    }
    IEnumerator MoveDown() {
        Vector3 newPos = body.transform.localPosition + Vector3.down;
        while (body.transform.localPosition != newPos) {
            body.transform.localPosition = Vector3.MoveTowards(body.transform.localPosition, newPos, speedUpDown * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(MoveDown());
        StartCoroutine(MoveUp());
        yield return null;
    }

}
