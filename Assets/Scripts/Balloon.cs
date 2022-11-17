using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public AudioClip boom;
    public GameObject body;
    public GameObject body1;
    public GameObject body2;
    private int value = 2;
    // Start is called before the first frame update
    private void Start()
    {
        Lift();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        AddForce();
        SelfDestroy();
    }

    // ABSTRACTION
    protected void SelfDestroy()
    {
        if (transform.position.y > 40 || transform.position.z > 50 || transform.position.z < -50 || transform.position.x > 50 || transform.position.x < -50)
        {
            Destroy(gameObject);
        }
    }
    private void Lift()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 0.03f, ForceMode.Impulse);
    }

    // ABSTRACTION
    protected virtual void DestroySeq()
    {
        if(boom != null)
        {
            gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(boom);
        }
        else
        {
            Debug.Log("didn't set boom sound.");
        }
        if(body != null)
        {
            body.SetActive(false);
        }
        if(body1 != null)
        {
            body1.SetActive(false);
        }
        if(body2 != null)
        {
            body2.SetActive(false);
        }
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(CheckIfAlive());
    }

    IEnumerator CheckIfAlive()
    {
            yield return new WaitForSeconds(0.5f);
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
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            //Debug.Log(collision.gameObject.name);
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
