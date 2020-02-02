using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string m_name;
    [SerializeField] private WorkType m_workRequired;

    public WorkType GetRequiredWork()
    {
        return m_workRequired;
    }

    public void WorkDone()
    {
        m_workRequired = WorkType.NONE;
    }
}
