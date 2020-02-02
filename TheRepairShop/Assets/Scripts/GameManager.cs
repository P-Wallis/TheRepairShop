using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    float levelTimer;
    float waitTimer;
    public GameObject ticket;
    public GameObject item;
    public Transform UICanvas;

    string[] ticketPrefabGUIDs;
    List<GameObject> ticketList;

    public static float ticketReductionIncrement = 0.01f;

    void Start()
    {
        levelTimer = 180;
        waitTimer = 0;

        ticketPrefabGUIDs = AssetDatabase.FindAssets("Ticket(");
        ticketList = new List<GameObject>();

        foreach (string guid in ticketPrefabGUIDs)
        {
            ticketList.Add(
                (GameObject)AssetDatabase.LoadAssetAtPath(
                    AssetDatabase.GUIDToAssetPath(guid), typeof(GameObject)
                )
            );
        }

        AddAnotherTicket();
    }

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
        //Debug.Log(waitTimer);
    }

    public void AddAnotherTicket()
    {
        var randInt = Random.Range(0, ticketList.Count);

        ticket = ticketList[randInt];

        Instantiate(ticket, UICanvas);
        GameObject itemGameObject = Instantiate(item) as GameObject;
        InRegion.instance.AddItemToQueue(itemGameObject);
        waitTimer = 3;
    }
}
