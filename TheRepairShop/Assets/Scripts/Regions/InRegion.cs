using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRegion : RegionBase
{
    public static InRegion instance;
    //public List<Item> m_items = new List<Item>();
    private Queue<Item> m_itemQueue = new Queue<Item>();

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void AddItemToQueue(Item item)
    {
        m_itemQueue.Enqueue(item);
        item.transform.position = transform.position + Vector3.up;
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
