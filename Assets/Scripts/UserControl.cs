using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserControl : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam1;
    public Texture2D cursor;
    public GameObject crossBow;
    public GameObject arrow;
    public GameObject arrowPrototype;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI arrowLoadedText;
    public AudioClip reloadSFX;
    public AudioClip getreadySFX;

    private int arrowNum;
    //private Ray mouse;
    private float rotateSpeed = 3.0f;
    private int round = 10;
    private int arrowRoundNum = 0;

    // ENCAPSULATION
    public int arrowLeft { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(256, 256), CursorMode.Auto);
        if(GameManager.Instance != null)
        {
            arrowNum = GameManager.Instance.ammo;
        }
        else
        {
            Debug.Log("GameManager.Instance is null.");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();
        ShootControl();
    }

    void RotateCamera()
    {
        if (cam1.transform.localRotation.eulerAngles.x < 300 && cam1.transform.localRotation.eulerAngles.x > 200 )
        {
            cam1.transform.localRotation = Quaternion.Euler(300, cam1.transform.localRotation.y, cam1.transform.localRotation.z);
        }
        else if (cam1.transform.localRotation.eulerAngles.x > 20 && cam1.transform.localRotation.eulerAngles.x < 90)
        {
            cam1.transform.localRotation = Quaternion.Euler(20, cam1.transform.localRotation.y, cam1.transform.localRotation.z);
        }
        cam1.transform.Rotate(Vector3.left * Input.GetAxis("Vertical") * rotateSpeed);
        cam.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);
    }

    public int CountArrow()
    {
        return arrowLeft = arrowNum + arrowRoundNum;
    }

    void ShootControl()
    {
        arrowRoundNum = crossBow.GetComponent<Animator>().GetInteger("Arrow");
        if(Input.GetKeyDown(KeyCode.R) && arrowRoundNum < 10 && arrowNum > 0)
        {
            if(arrowNum > round - arrowRoundNum)
            {
                arrowNum -= round - arrowRoundNum;
                arrowRoundNum = round;
            }
            else
            {
                arrowRoundNum += arrowNum;
                arrowNum = 0;
            }
            crossBow.GetComponent<Animator>().SetBool("FireReady", true);
            crossBow.GetComponent<Animator>().SetInteger("Arrow", arrowRoundNum);
            crossBow.GetComponent<Animator>().SetTrigger("Refill");
            GetComponent<AudioSource>().PlayOneShot(reloadSFX);
            arrowPrototype.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && arrowRoundNum > 0 && crossBow.GetComponent<Animator>().GetBool("FireReady"))
        {
            crossBow.GetComponent<Animator>().SetBool("FireReady", false);
            GameObject arr = Instantiate(arrow,crossBow.transform.position,arrow.transform.rotation);
            arrowRoundNum--;
            crossBow.GetComponent<Animator>().SetInteger("Arrow", arrowRoundNum);
            arrowPrototype.SetActive(false);
        }
        if(Input.GetMouseButtonDown(1) && !crossBow.GetComponent<Animator>().GetBool("FireReady") && arrowRoundNum > 0)
        {
            crossBow.GetComponent<Animator>().SetBool("FireReady", true);
            GetComponent<AudioSource>().PlayOneShot(getreadySFX);
            arrowPrototype.SetActive(true);
        }
        arrowText.text = "Total Arrow: " + CountArrow();
        arrowLoadedText.text = "Arrow Load:" + arrowRoundNum;
    }



}
