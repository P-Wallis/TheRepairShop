﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    RegionBase m_currentRegion = null;
    Item m_heldItem = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == RegionBase.RegionTag)
        {
            m_currentRegion = other.GetComponent<RegionBase>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == RegionBase.RegionTag)
        {
            RegionBase otherRegion = other.GetComponent<RegionBase>();
            if (otherRegion == m_currentRegion)
                m_currentRegion = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_currentRegion != null)
        {
            Item oldItem = m_heldItem;
            m_heldItem = m_currentRegion.ItemInteraction(m_heldItem);
            if (m_heldItem != null)
            {
                Debug.Log("The player is now holding '" + m_heldItem.m_name + "'!");
                if (oldItem != m_heldItem)
                {
                    AudioPlayer.Instance.PlayAudioOnce("ItemPickup");
                    m_heldItem.MoveToPosition(transform, Vector3.up * 2.5f);
                }
            }
            else
            {
                Debug.Log("The player is not holding anything.");
            }
            if (oldItem != null && m_heldItem == null)
                AudioPlayer.Instance.PlayAudioOnce("ItemPutdown");
        }
    }

}
