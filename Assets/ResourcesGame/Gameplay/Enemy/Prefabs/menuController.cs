using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject town;
    float minX = -10;
    float maxX = 10;

    float minZ = -5;
    float maxZ = 5;

    private void Start() {

        Create();
    }
    IEnumerator corr() {
        yield return new WaitForSeconds(5f);
        StopCoroutine(corr());
        Create();
        yield return null;
    }

    void Create() {
        for (int i = 0; i < 3; i++) {
            GameObject go = Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ)), Quaternion.identity);
            go.GetComponent<movMenu>().SetTown(town);
        }
        StartCoroutine(corr());
    }

}
