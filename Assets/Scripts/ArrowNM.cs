using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNM : MonoBehaviour
{
    private bool IsShoot = false;
    private float speedNM = 10f;
    private float m_forceFactor = 0.1f;
    private GameObject arrowPrototype;

    public virtual float forceFactor
    {
        get
        {
            return m_forceFactor;
        }
        set
        {
            if(forceFactor < 5 && forceFactor > 0)
            {
                m_forceFactor = forceFactor;
            }
        }
    }

    private void Awake()
    {
        arrowPrototype = GameObject.Find("ArrowPrototype");
    }

    private void Update()
    {
        if (!IsShoot)
        {
            transform.position = arrowPrototype.transform.position;
            transform.rotation = arrowPrototype.transform.rotation * Quaternion.Euler(90, 0, 0);
        }
        if((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) && !IsShoot)
        {
            Shoot();
            IsShoot = true;
        }
        if(transform.position.y < -30 || transform.position.y > 500)
        {
            Destroy(gameObject);
        }
        else if(transform.position.z < -500 || transform.position.y > 500)
        {
            Destroy(gameObject);
        }
        
    }

    public void Shoot()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speedNM * forceFactor, ForceMode.Impulse);
    }

    public virtual void Recycle()
    {
        
    }
}
