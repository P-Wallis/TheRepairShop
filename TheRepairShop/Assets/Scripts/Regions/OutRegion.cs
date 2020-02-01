using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRegion : RegionBase
{
    private List<Item> m_items = new List<Item>();

    public override Item ItemInteraction(Item playerItem)
    {
        if (playerItem != null)
        {
            m_items.Add(playerItem);
            playerItem.transform.parent = transform;
            playerItem.transform.localPosition = Vector3.up * (m_items.Count);
        }

        return null;
    }
}
