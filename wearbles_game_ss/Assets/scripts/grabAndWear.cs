using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabAndWear : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GameManager.instance.wearables[GameManager.instance.gameProgress] && !GameManager.instance.canGrab)
        {
            GameManager.instance.grabbableObject = collision.gameObject;
            GameManager.instance.grabbableObject.SetActive(true);
            GameManager.instance.canGrab = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameManager.instance.wearables[GameManager.instance.gameProgress])
        {
            GameManager.instance.canGrab = false;
        }
    }
}
