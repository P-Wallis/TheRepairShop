using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float levelTimer;
    float waitTimer;
    public GameObject ticket;
    public GameObject item;
    public Transform UICanvas;

    // Start is called before the first frame update
    void Start()
    {
        levelTimer = 180;
        waitTimer = 0;
        AddAnotherTicket();
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;
        if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            AddAnotherTicket();
        }
        //Debug.Log(levelTimer);
        Debug.Log(waitTimer);
    }

    public void AddAnotherTicket()
    {
        Instantiate(ticket, UICanvas);
        GameObject itemGameObject = Instantiate(item) as GameObject;
        InRegion.instance.AddItemToQueue(itemGameObject);
        waitTimer = 3;
    }
}
