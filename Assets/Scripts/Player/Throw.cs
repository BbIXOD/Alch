using System;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [NonSerialized] public int Charge;
    [SerializeField] private GameObject throwable;

    private void Update()
    {
        if (Charge > 5) Charge = 5;
        if (!(Input.GetKeyDown(KeyCode.Q) && Charge == 5)) return;
        Charge = 0;
        Instantiate(throwable, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
