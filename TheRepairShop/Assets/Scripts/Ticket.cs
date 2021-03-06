﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Ticket : MonoBehaviour
{
    [HideInInspector] public Item item;
    WorkType type;

    public Image custPortImgSrc, itemImgSrc;

    string[] custPortImgGUIDs, itemImgGUIDs;
    public List<Sprite> custPortList;

    public Slider sliderSrc;
    public Image fillSrc;
    float changePortraitTimer = Mathf.Epsilon;

    /// <summary>
    /// We should decide how to determine this value; via Ticket or ItemDifficultyData
    /// </summary>
    [Range(1, 20)] public float timeLimit = 10;

    public enum CustomerImage { happy, neutral, sad };
    public CustomerImage curCustImg = CustomerImage.happy;

    [HideInInspector]public enum ItemType { radio, guitar, TV, clock };
    public ItemType itemType;
    string itemTypeID;

    //enum ItemImage { red, green, blue };

    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer.Instance.PlayAudioOnce("Bell1");

        InitializeVars();

        AssignTicketTypeImage();
        //AssignRandomImage();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCustomerPortrait();
        //DestroyOnFail();
    }

    void InitializeVars()
    {
        type = item.GetRequiredWork();

        

    }

   

    void ChangeCustomerPortrait()
    {
        if (!GameManager.instance.IsGameRunning)
            return;
        changePortraitTimer += Time.deltaTime;

        if (changePortraitTimer >= GameManager.instance.ticketReductionIncrement)
        {
            changePortraitTimer = Mathf.Epsilon;
            if (curCustImg == CustomerImage.happy)
                sliderSrc.value -= GameManager.instance.ticketReductionIncrement / timeLimit;
            else
                sliderSrc.value -= GameManager.instance.ticketReductionIncrement / timeLimit * GameManager.instance.ticketReductionMultiplier;
        }

        if (sliderSrc.value >= 0.66)
            fillSrc.color = Color.green;
        else if (sliderSrc.value < 0.66 && sliderSrc.value >= 0.33)
            fillSrc.color = Color.yellow;
        else
            fillSrc.color = Color.red;

        if (sliderSrc.value <= 0)
        {
            if (curCustImg == CustomerImage.happy)
            {
                curCustImg = CustomerImage.neutral;


                switch (itemType)
                {
                    case ItemType.radio:
                        itemTypeID = "2_";
                        break;
                    case ItemType.guitar:
                        itemTypeID = "1_";
                        break;
                    case ItemType.TV:
                        itemTypeID = "3_";
                        break;
                    case ItemType.clock:
                        itemTypeID = "4_";
                        break;
                    default:
                        Debug.Log("None");
                        break;
                }

                Debug.Log(itemTypeID);

                custPortImgSrc.sprite = custPortList.Find(item => item.name.Contains(itemTypeID + "Neutral"));  //"Neutral_"
            }
            else if (curCustImg == CustomerImage.neutral)
            {
                curCustImg = CustomerImage.sad;
                custPortImgSrc.sprite = custPortList.Find(item => item.name.Contains(itemTypeID + "Angry"));  //"Sad_"
            }

            if(curCustImg != CustomerImage.sad)
            sliderSrc.value = 1;
        }
    }

    void AssignTicketTypeImage()
    {
        /* This does not do anything now because there is only one ticket.
        if (type == WorkType.CARPENTER)
        {
            print("this is for carpenter");
        }
        else
            print("this is not for carpenter");
            */
    }

    public void Complete() { 
    if(curCustImg!=CustomerImage.sad)
            AudioPlayer.Instance.PlayAudioOnce("ScoreGain");
    else
            AudioPlayer.Instance.PlayAudioOnce("ScoreLose");
    }

    //void AssignRandomImage()
    //{
    //    var randInt = Random.Range(0, custPortList.Count);
    //    //custPortImgSrc.sprite = custPortList[randInt];

    //    //randInt = Random.Range(0, itemImgList.Count);
    //    itemImgSrc.sprite = itemImgList[randInt];
    //}

    //void DestroyOnFail()
    //{
    //    if (curProgress <= 0) Destroy(gameObject);
    //}
}
