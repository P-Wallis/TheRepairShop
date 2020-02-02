using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRegion : RegionBase
{
    private List<Item> m_items = new List<Item>();

    void Start()
    {
        StartCoroutine(CoRemoveItems());
    }

    public override Item ItemInteraction(Item playerItem)
    {
        if (playerItem != null && playerItem.GetRequiredWork() == WorkType.NONE)
        {
            m_items.Add(playerItem);
            playerItem.MoveToPosition(transform, Vector3.left * ((m_items.Count-2)*2));
            GameManager.instance.TicketComplete(playerItem.ticket);
            return null;
        }

        return playerItem;
    }

    IEnumerator CoRemoveItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            if (m_items.Count > 0)
            {
                int randomID = Random.Range(0, m_items.Count);
                Item removed = m_items[randomID];
                m_items.RemoveAt(randomID);

                for (int i = 0; i < m_items.Count; i++)
                {
                    m_items[i].MoveToPosition(transform, Vector3.left * ((i - 1)*2));
                }

                removed.MoveToPosition(transform, removed.transform.localPosition + (Vector3.back * 30));
                yield return new WaitForSeconds(2);
                Destroy(removed.gameObject);
            }
        }
    }
}