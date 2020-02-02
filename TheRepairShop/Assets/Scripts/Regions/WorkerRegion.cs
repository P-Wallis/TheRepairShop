using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkType
{
    NONE = -1,
    CARPENTER,
    MECHANIC,
    PAINTER
}

public class WorkerRegion : RegionBase
{
    public WorkType m_type = WorkType.CARPENTER;
    public float m_worktime = 2f;

    private Item m_item = null;
    private bool m_working = false;

    public override Item ItemInteraction(Item playerItem)
    {
        if (m_item == null && playerItem != null && playerItem.GetRequiredWork() == m_type)
        {
            m_item = playerItem;
            StartCoroutine(Work());
            return null;
        }

        else if (m_item != null && !m_working)
        {
            Item doneItem = m_item;
            m_item = (playerItem !=null && playerItem.GetRequiredWork() == m_type) ? playerItem : null;
            if (m_item != null)
                StartCoroutine(Work());
            return doneItem;
        }

        return playerItem;
    }


    IEnumerator Work()
    {
        m_working = true;
        m_item.MoveToPosition(transform, Vector3.zero);
        Debug.Log("Working...");
        yield return new WaitForSeconds(m_worktime);
        m_item.MoveToPosition(transform, Vector3.up);
        m_item.WorkDone();
        Debug.Log("Done!");
        m_working = false;
    }
}
