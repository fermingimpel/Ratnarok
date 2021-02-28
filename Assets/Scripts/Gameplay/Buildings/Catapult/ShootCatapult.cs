using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCatapult : Bullet {
    [SerializeField] Vector3 initPos;
    [SerializeField] Vector3 midPos;
    [SerializeField] Vector3 finalPos;
    [SerializeField] float distanceToMove;

    bool goingUp = true;
    [SerializeField] bool moving = false;
    private void Start() {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        initPos = transform.position;
        finalPos = direction * distanceToMove + initPos;
        midPos = Vector3.Lerp(initPos, finalPos, 0.33f) + (Vector3.up * 1.5f);
        moving = true;
    }

    private void Update() {
        if (!moving)
            return;

        if (goingUp) {
            transform.position = Vector3.MoveTowards(transform.position, midPos, speed * Time.deltaTime);
            if (transform.position == midPos)
                goingUp = false;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, finalPos, speed * Time.deltaTime);

        if (transform.position == finalPos)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(this.gameObject, 0.05f);
        }
    }
}
