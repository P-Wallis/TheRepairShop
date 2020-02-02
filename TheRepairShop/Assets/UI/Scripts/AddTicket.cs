using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTicket : MonoBehaviour
{
    public GameObject ticket;
    public GameObject item;

    public void AddAnotherTicket()
    {
        Instantiate(ticket, transform.parent);
        GameObject itemGameObject = Instantiate(item) as GameObject;
        InRegion.instance.AddItemToQueue(itemGameObject.GetComponent<Item>());
    }
}
