using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private int value = 2;
    // Start is called before the first frame update
    private void Start()
    {
        Lift();
    }

    // Update is called once per frame
    private void Update()
    {
        AddForce();
        
    }

    protected void SelfDestroy()
    {
        if (transform.position.y > 150 || transform.position.z > 200)
        {
            DestroySeq();
        }
    }
    private void Lift()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 0.03f, ForceMode.Impulse);
    }

    protected virtual void DestroySeq()
    {
        Destroy(gameObject);
    }

    protected virtual int GetScore()
    {
        return value;
    }

    protected virtual void AddForce()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.5)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 0.03f, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Debug.Log("Hit Balloom");
            if(GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(GetScore());
                DestroySeq();
            }
            else
            {
                Debug.Log("GameManager.Instance is null.");
            }
        }
    }
}
