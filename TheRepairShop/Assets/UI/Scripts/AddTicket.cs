﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTicket : MonoBehaviour
{
    public GameObject ticket;

    public void AddAnotherTicket()
    {
        Instantiate(ticket, transform.parent);
    }
}
