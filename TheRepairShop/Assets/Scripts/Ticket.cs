using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Ticket : MonoBehaviour
{
    Image custPortImgSrc, itemImgSrc;

    string[] custPortImgGUIDs, itemImgGUIDs;
    List<Sprite> custPortList, itemImgList;

    enum CustomerImage { happy, neutral, sad };
    enum ItemImage { red, green, blue };

    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer.Instance.PlayAudioOnce("Bell1");

        InitializeVars();

        AssignRandomImage();

        // Experimental code:
        CustomerImage curCustImg;
        curCustImg = CustomerImage.happy;

        ItemImage curItemImg;
        curItemImg = ItemImage.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeVars()
    {
        custPortImgSrc = gameObject.GetComponentsInChildren<Image>()[1];
        itemImgSrc = gameObject.GetComponentsInChildren<Image>()[2];

        custPortImgGUIDs = AssetDatabase.FindAssets("-CustomerPortrait-", new[] { "Assets/UI/Images" });
        itemImgGUIDs = AssetDatabase.FindAssets("-ItemImage-", new[] { "Assets/UI/Images" });

        custPortList = new List<Sprite>();
        itemImgList = new List<Sprite>();

        BuildImageLists();
    }

    void BuildImageLists()
    {
        var flag = true;

        foreach (string guid in custPortImgGUIDs)
        {
            if (flag)
                custPortList.Add(
                    (Sprite)AssetDatabase.LoadAssetAtPath(
                        AssetDatabase.GUIDToAssetPath(guid), typeof(Sprite)
                    )
                );
            flag = !flag;
        }

        foreach (string guid in itemImgGUIDs)
        {
            if (flag)
                itemImgList.Add(
                    (Sprite)AssetDatabase.LoadAssetAtPath(
                        AssetDatabase.GUIDToAssetPath(guid), typeof(Sprite)
                    )
                );
            flag = !flag;
        }
    }

    void AssignRandomImage()
    {
        var randInt = Random.Range(0, custPortList.Count);
        custPortImgSrc.sprite = custPortList[randInt];

        randInt = Random.Range(0, itemImgList.Count);
        itemImgSrc.sprite = itemImgList[randInt];
    }
}
