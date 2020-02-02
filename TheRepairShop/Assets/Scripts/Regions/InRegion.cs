using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRegion : RegionBase
{
    public static InRegion instance;
    //public List<Item> m_items = new List<Item>();
    private Queue<Item> m_itemQueue = new Queue<Item>();
    private Transform itemsParent;

    public override void Awake()
    {
        base.Awake();
        instance = this;
        GameObject itemsParentGO = new GameObject() { name = "ItemsParent" };
        itemsParent = itemsParentGO.transform;
        itemsParent.parent = transform;
        itemsParent.localPosition = Vector3.zero;
    }

    public void AddItemToQueue(Item item)
    {
        Item iteminqueue;
        for (int i = 0; i < itemsParent.childCount; i++)
        {
            iteminqueue = itemsParent.GetChild(i).GetComponent<Item>();
            iteminqueue.MoveToPosition(itemsParent, Vector3.right * i);
        }

        m_itemQueue.Enqueue(item);
        item.transform.position = itemsParent.position + (Vector3.right * 10);
        item.MoveToPosition(itemsParent, Vector3.right * itemsParent.childCount);
        item.transform.localRotation = Quaternion.Euler(0, Random.Range(-180f,180f), 0);
    }


    public override Item ItemInteraction(Item playerItem)
    {
        if (playerItem != null)
            return playerItem;

        if (m_itemQueue.Count > 0)
            return m_itemQueue.Dequeue();

        return null;
    }
}
