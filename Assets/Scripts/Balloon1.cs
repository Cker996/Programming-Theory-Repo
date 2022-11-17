using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Balloon1 : Balloon
{
    private int value1 = 4;

    // POLYMORPHISM
    protected override int GetScore()
    {
        return value1;
    }

    // POLYMORPHISM
    protected override void AddForce()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 0.05f, ForceMode.Impulse);
        }
    }
}
