using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Balloon3 : Balloon
{
    private int value3 = 8;

    // POLYMORPHISM
    protected override void AddForce()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 1.0f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 0.03f, ForceMode.Impulse);
            
        }
        transform.RotateAround(Vector3.zero, Vector3.up, 0.45f);
    }

    // POLYMORPHISM
    protected override int GetScore()
    {
        return value3;
    }

}
