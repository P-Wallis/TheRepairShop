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
    public float m_worktime = 5f;

    private Item m_item = null;
    private bool m_working = false;

    public Transform progressBar;

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
        progressBar.localScale = new Vector3(1f, 0.2f, 1f);

        m_working = true;
        m_item.transform.parent = transform;
        m_item.transform.localPosition = Vector3.zero;
        Debug.Log("Working...");

        float t = 0f;
        Vector3 startScale = progressBar.localScale;
        while (t < m_worktime)
        {
            t += Time.deltaTime;
            progressBar.localScale = Vector3.Slerp(startScale, new Vector3(0, 0.2f, 1f), t / m_worktime);
            yield return null;
        };

        //yield return new WaitForSeconds(m_worktime);
        m_item.transform.localPosition = Vector3.up;
        m_item.WorkDone();
        Debug.Log("Done!");
        m_working = false;
    }
}
