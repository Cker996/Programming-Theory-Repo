using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Balloon2 : Balloon
{
    private int value2 = 1;

    // POLYMORPHISM
    protected override void AddForce()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.5)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 0.03f, ForceMode.Impulse);
        }
    }

    // POLYMORPHISM
    protected override int GetScore()
    {
        return value2;
    }

    // POLYMORPHISM
    protected override void OnCollisionEnter(Collision collision1)
    {
        if (collision1.gameObject.CompareTag("Arrow"))
        {
            //Debug.Log(collision.gameObject.name);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(GetScore());
                if (boom != null)
                {
                    gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(boom);
                }
            }
            else
            {
                Debug.Log("GameManager.Instance is null.");
            }
        }
    }
}
