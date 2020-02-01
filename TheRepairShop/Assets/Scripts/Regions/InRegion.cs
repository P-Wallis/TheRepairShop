using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRegion : RegionBase
{
    public List<Item> m_items = new List<Item>();
    private Queue<Item> m_itemQueue = new Queue<Item>();

    private void Start()
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            m_itemQueue.Enqueue(m_items[i]);
            m_items[i].transform.position = transform.position + (Vector3.up * (m_items.Count - i));
        }
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
