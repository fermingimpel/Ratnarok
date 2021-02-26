using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseMoney : MonoBehaviour
{

    public delegate void MoneyGrabbed(float c);
    public static event MoneyGrabbed GrabbedMoney;
    bool grabbed = false;
    [SerializeField] float cheesePerCoin;
    [SerializeField] float posY;
    [SerializeField] float timeToDestroy;
    private void Start() {
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        Destroy(this.gameObject, timeToDestroy);
    }
    public void SetCheesePerCoinc(float c) {
        cheesePerCoin = c;
    }
    public void GrabMoney() {
        if (grabbed) return;

        grabbed = true;
        if (GrabbedMoney != null)
            GrabbedMoney(cheesePerCoin);
        Destroy(this.gameObject);
    }

}
