using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject Counters;
    int a = 0;
    int s = 0;
    int d = 0;
    int w = 0;
    int leftShift = 0;
    int space = 0;
    bool aDown;
    bool sDown;
    bool dDown;
    bool wDown;
    void updateCounts()
    {
        Counters.transform.GetChild(1).GetComponent<TMP_Text>().text = "A : " + a;
        Counters.transform.GetChild(2).GetComponent<TMP_Text>().text = "S : " + s;
        Counters.transform.GetChild(3).GetComponent<TMP_Text>().text = "D : " + d;
        Counters.transform.GetChild(4).GetComponent<TMP_Text>().text = "W : " + w;
        Counters.transform.GetChild(5).GetComponent<TMP_Text>().text = "Left Shift : " + leftShift;
        Counters.transform.GetChild(6).GetComponent<TMP_Text>().text = "Space : " + space;
    }
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
        updateCounts();
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
            if (!aDown)
            {
                aDown = true;
                a++;
                updateCounts();
            }
            Vector3 destination = new Vector3(hand.transform.position.x - ropeMoveSpeed * Time.deltaTime, hand.transform.position.y, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!sDown)
            {
                sDown = true;
                s++;
                updateCounts();
            }
            Vector3 destination = new Vector3(hand.transform.position.x, hand.transform.position.y - ropeMoveSpeed * Time.deltaTime, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (!wDown)
            {
                wDown = true;
                w++;
                updateCounts();
            }
            Vector3 destination = new Vector3(hand.transform.position.x, hand.transform.position.y + ropeMoveSpeed * Time.deltaTime, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!dDown)
            {
                dDown = true;
                d++;
                updateCounts();
            }
            Vector3 destination = new Vector3(hand.transform.position.x + ropeMoveSpeed * Time.deltaTime, hand.transform.position.y, hand.transform.position.z);
            hand.transform.position = destination;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aDown = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            sDown = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dDown = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            wDown = false;
        }
    }
    void manageGrab()
    {
        if(canGrab)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                grabbed = true;
                leftShift++;
                updateCounts();
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
                space++;
                updateCounts();
                drawer.transform.GetChild(gameProgress).gameObject.SetActive(true);
                status.text = "Grabbed nothing";
                Destroy(rackdrawer.transform.GetChild(0).gameObject);
                gameProgress++;
                Debug.Log(gameProgress);
                if(gameProgress == 4)
                {
                    status.text = "Congratulations !!! \n you did great...";
                    Invoke("updateStatus", 1);
                    Invoke("startNewGame", 4);
                }
            }
        }
    }
    void updateStatus()
    {
        status.text = "Starting again!!!";    
    }
    void startNewGame()
    {
        SceneManager.LoadScene(0);
    }
}
