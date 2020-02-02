using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationMeter : MonoBehaviour
{
    [SerializeField] Image[] m_stars;
    public Sprite m_starFull, m_starHalf, m_starEmpty;

    float m_currentRep;
    void SetMeter(float value)
    {
        for (int i = 0; i < m_stars.Length; i++)
        {
            if (value > (i + .75f))
                m_stars[i].sprite = m_starFull;
            else if (value < (i + .25f))
                m_stars[i].sprite = m_starEmpty;
            else
                m_stars[i].sprite = m_starHalf;
        }
        m_currentRep = value;
        Debug.Log("Rep: " + m_currentRep);
    }

    private void Start()
    {
        SetMeter(Random.Range(0f, 5f));
    }
}
