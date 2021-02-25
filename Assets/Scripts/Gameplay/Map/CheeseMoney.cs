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

    public void GrabMoney() {
        if (grabbed) return;

        grabbed = true;
        if (GrabbedMoney != null)
            GrabbedMoney(cheesePerCoin);
        Destroy(this.gameObject);
    }

}
