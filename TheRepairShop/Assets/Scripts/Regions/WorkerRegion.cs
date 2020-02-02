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
    public Animator m_animator;

    void Start()
    {
        progressBar.localScale = new Vector3(0, 0.2f, 1f);
    }

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
            if ((playerItem != null && playerItem.GetRequiredWork() == m_type) || playerItem == null)
            {
                Item doneItem = m_item;
                m_item = playerItem;
                if (m_item != null)
                    StartCoroutine(Work());
                return doneItem;
            }
        }

        return playerItem;
    }

    IEnumerator Work()
    {
        progressBar.localScale = new Vector3(1f, 0.2f, 1f);
        int AudioID = 0;
        m_working = true;
        m_item.MoveToPosition(transform, Vector3.zero);
        if(m_animator!=null)
            m_animator.SetBool("Working", true);
        switch (m_type)
        {
            case WorkType.NONE:
                break;
            case WorkType.CARPENTER:
                AudioID = AudioPlayer.Instance.PlayAudioLoop("AnvilHammering");
                break;
            case WorkType.MECHANIC:
                AudioID = AudioPlayer.Instance.PlayAudioLoop("Screwdriver");
                break;
            case WorkType.PAINTER:
                AudioID = AudioPlayer.Instance.PlayAudioLoop("Welding");
                break;
        }

        float t = 0f;
        Vector3 startScale = progressBar.localScale;
        while (t < m_worktime)
        {
            t += Time.deltaTime;
            progressBar.localScale = Vector3.Slerp(startScale, new Vector3(0, 0.2f, 1f), t / m_worktime);
            yield return null;
        };

        m_item.MoveToPosition(transform, Vector3.up);
        m_item.WorkDone();
        if(AudioID!=0)
        AudioPlayer.Instance.StopAudioLoop(AudioID);
        AudioPlayer.Instance.PlayAudioOnce("Alert");
        if (m_animator != null)
            m_animator.SetBool("Working", false);
        m_working = false;
    }
}
