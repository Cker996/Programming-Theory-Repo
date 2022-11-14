using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject pointer;
    public GameObject hand;

    private float distance = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        hand.transform.rotation = Quaternion.LookRotation(GetMouseRay().direction);
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseRay();
    }

    public Ray GetMouseRay()
    {
        Ray mouse;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //Vector3 focus = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        mouse = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouse, out hit, distance))
        {
            GetComponent<LineRenderer>().SetPosition(1, hit.point);
            pointer.transform.position = mouse.direction * hit.distance * 0.99f + mouse.origin;
            /*
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("pointer " + pointer.transform.position);
                Debug.Log("hit point " + hit.point);
                Debug.DrawRay(mouse.origin, mouse.direction * 10, Color.yellow,15.0f);
            }*/
        }
        else
        {
            GetComponent<LineRenderer>().SetPosition(1, mouse.direction * 30 + mouse.origin);
            pointer.transform.position = mouse.direction * 30 + mouse.origin;
        }
        hand.transform.rotation = Quaternion.LookRotation(mouse.direction);
        return mouse;
    }
}
