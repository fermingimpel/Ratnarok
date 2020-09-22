using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movMenu : MonoBehaviour
{
    [SerializeField] GameObject town;
    Vector3 dir;
    float minX = -15;
    float maxX = 15;

    float minZ = -15;
    float maxZ = 15;

    Vector3 initPos;

    float speed = 5;
    void Start()
    {
        initPos = transform.position;
        transform.position += Vector3.up * 0.75f;
    }
    bool going = true;

    IEnumerator Move() {
        transform.LookAt(town.transform.position);
        while(going) {
            transform.position = Vector3.MoveTowards(transform.position, town.transform.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        transform.LookAt(initPos);
        while (transform.position != initPos) {
            transform.position = Vector3.MoveTowards(transform.position, initPos, speed * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(Move());
        Destroy(this.gameObject);

    }

    public void SetTown(GameObject t) {
        town = t;
        StartCoroutine(Move());
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Town") {
            going = false;
        }
    }

}
