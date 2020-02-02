using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRegion : RegionBase
{
    private List<Item> m_items = new List<Item>();

    public override Item ItemInteraction(Item playerItem)
    {
        if (playerItem != null && playerItem.GetRequiredWork() == WorkType.NONE)
        {
            m_items.Add(playerItem);
            playerItem.MoveToPosition(transform, Vector3.up * (m_items.Count));
            GameManager.instance.TicketComplete(playerItem.ticket);
            return null;
        }

        return playerItem;
    }
}