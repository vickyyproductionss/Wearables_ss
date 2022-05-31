using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject hand;
    public float ropeMoveSpeed;
    public static GameManager instance;
    public int gameProgress = 0;
    public List<string> wearables = new List<string>();
    public GameObject grabbableObject;
    public bool canGrab;
    bool grabbed;
    public GameObject drawer;
    public GameObject rackdrawer;
    public TMP_Text status;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        wearables.Add("dress");
        wearables.Add("cap");
        wearables.Add("mask");
        wearables.Add("glasses");
    }

    // Update is called once per frame
    void Update()
    {
        manageHook();
        manageGrab();
    }
    void manageHook()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 destination = new Vector3(hand.transform.position.x - ropeMoveSpeed * Time.deltaTime, hand.transform.position.y, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 destination = new Vector3(hand.transform.position.x, hand.transform.position.y - ropeMoveSpeed * Time.deltaTime, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 destination = new Vector3(hand.transform.position.x, hand.transform.position.y + ropeMoveSpeed * Time.deltaTime, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 destination = new Vector3(hand.transform.position.x + ropeMoveSpeed * Time.deltaTime, hand.transform.position.y, hand.transform.position.z);
            hand.transform.position = destination;
        }
    }
    void manageGrab()
    {
        if(canGrab)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                grabbed = true;
                status.text = "Grabbed " + wearables[gameProgress];
            }
        }
        if(grabbed)
        {
            grabbableObject.transform.position = hand.transform.position;
        }
        if(grabbed)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hand.transform.position.x > 1)
            {
                grabbed = false;
                drawer.transform.GetChild(gameProgress).gameObject.SetActive(true);
                status.text = "Grabbed nothing";
                Destroy(rackdrawer.transform.GetChild(0).gameObject);
                gameProgress++;
                Debug.Log(gameProgress);
                if(gameProgress == 4)
                {
                    status.text = "Congratulations !!! \n you did great...";
                }
            }
        }
    }
}
