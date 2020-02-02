using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int CurrentLevel = 0;
    float levelTimer;
    float waitTimer;
    public GameObject ticket;
    public List<Item> items;
    public Transform UICanvas;
    [SerializeField] LevelSerializationObject LevelData;
    List<GameObject> ticketList;
    [HideInInspector] public List<Ticket> pendingTickets = new List<Ticket>();
    [HideInInspector] public List<Ticket> completedTickets = new List<Ticket>();

    [Range(0.01f, 0.25f)] public float ticketReductionIncrement = 0.01f;
    [Range(1f, 2f)] public float ticketReductionMultiplier = 1.5f;
    [Range(1f, 50f)]public float itemSpeed = 10f;

    private void Awake()
    {
        instance = this;

        levelTimer = 180;
        waitTimer = 0;

        string[] ticketPrefabGUIDs = AssetDatabase.FindAssets("Ticket(");
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
    }

    public void AddAnotherTicket()
    {
        if (pendingTickets.Count < 6 && completedTickets.Count<18)
        {
            var randInt = Random.Range(0, ticketList.Count);

            ticket = ticketList[randInt];

            GameObject ticketGameObject = Instantiate(ticket, UICanvas);

            //We Choose Item to instantiate according to LevelData.
            GameObject itemGameObject = Instantiate(items.Find((x)=>x.m_name == LevelData.Levels[CurrentLevel].ChooseOneItem())).gameObject;

            Ticket ticketScript = ticketGameObject.GetComponent<Ticket>();
            ticketScript.item = itemGameObject.GetComponent<Item>();
            ticketScript.item.ticket = ticketScript;
            pendingTickets.Add(ticketScript);

            InRegion.instance.AddItemToQueue(ticketScript.item);
            waitTimer = LevelData.Levels[CurrentLevel].ItemIncomeTermSeconds;
        }
    }

    public void TicketComplete(Ticket completedTicket)
    {
        completedTicket.gameObject.SetActive(false);
        pendingTickets.Remove(completedTicket);
        completedTickets.Add(completedTicket);
    }
    public void NextLevel() {
        if (CurrentLevel + 1 < LevelData.Levels.Count) CurrentLevel++;
        else GameEnd();
    }

    public void GameEnd() { 
    
    }
}
