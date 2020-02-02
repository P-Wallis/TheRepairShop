using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int CurrentLevel = 0;
    int TotalReceivedItems = 0;
    int TotalSuccess = 0;
    public float levelLength => LevelData.Levels[CurrentLevel].LevelLengthSeconds;
    public float levelTimer;
    float waitTimer;
    public bool IsGameRunning = true;
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

    private float reputation = 2.5f;
    public float Reputation { get { return reputation; } set { reputation = value; OnReputationUpdate?.Invoke(); } }
    private int numberOfReviews = 2;
    public event Action OnReputationUpdate;

    private void Awake()
    {
        instance = this;

        levelTimer = LevelData.Levels[CurrentLevel].LevelLengthSeconds;
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
    private void Start()
    {

        AudioPlayer.Instance.PlayAudioOnce("GameStart");
        AudioPlayer.Instance.PlayAudioLoop("BackgroundMusic1");
    }

    void Update()
    {
        if(IsGameRunning)
        levelTimer -= Time.deltaTime;

        if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else
        {
            if(IsGameRunning)
            AddAnotherTicket();
        }
        if (levelTimer < 0) {

            LevelEnd();

        }
    }

    public void UpdateReputationWithNewReview(float reviewScore)
    {
        numberOfReviews++;
        float newReviewPercentage = 1f / numberOfReviews;
        reputation = Mathf.Lerp(reputation, reviewScore, newReviewPercentage);

        if (OnReputationUpdate != null)
            OnReputationUpdate();
    }

    public void AddAnotherTicket()
    {
        if (pendingTickets.Count < 6 && completedTickets.Count<18)
        {
            var randInt = Random.Range(0, ticketList.Count);

            ticket = ticketList[randInt];

            GameObject ticketGameObject = Instantiate(ticket, UICanvas);

            //We Choose Item to instantiate according to LevelData.
            var itemDifficultyData = LevelData.Levels[CurrentLevel].ChooseOneItem();
            GameObject itemGameObject = Instantiate(items.Find((x)=>x.m_name == itemDifficultyData.ItemName)).gameObject;

            Ticket ticketScript = ticketGameObject.GetComponent<Ticket>();
            ticketScript.timeLimit = itemDifficultyData.TimeGiven;
            ticketScript.item = itemGameObject.GetComponent<Item>();
            ticketScript.item.ticket = ticketScript;
            pendingTickets.Add(ticketScript);

            InRegion.instance.AddItemToQueue(ticketScript.item);
            waitTimer = LevelData.Levels[CurrentLevel].ItemIncomeTermSeconds;
        }
        TotalReceivedItems++;
    }

    public void TicketComplete(Ticket completedTicket)
    {
        completedTicket.gameObject.SetActive(false);
        completedTicket.Complete();
        pendingTickets.Remove(completedTicket);
        completedTickets.Add(completedTicket);

        switch (completedTicket.curCustImg)
        {
            case Ticket.CustomerImage.happy:
                UpdateReputationWithNewReview(5);
                break;
            case Ticket.CustomerImage.neutral:
                UpdateReputationWithNewReview(3);
                break;
            case Ticket.CustomerImage.sad:
                UpdateReputationWithNewReview(1);
                break;
        }

        TotalSuccess++;
    }

    /// <summary>
    /// Called when the level ends
    /// </summary>
    public void LevelEnd() {
        IsGameRunning = false;
        LevelResultPanel.Instance.LevelEnd(TotalReceivedItems, TotalSuccess);

    }

    /// <summary>
    /// Currently not called. Called when the next level starts
    /// </summary>
    public void NextLevel() {
        if (CurrentLevel + 1 < LevelData.Levels.Count) CurrentLevel++;
        else GameEnd();
    }

    public void GameEnd()
    { 
    
    }
}
