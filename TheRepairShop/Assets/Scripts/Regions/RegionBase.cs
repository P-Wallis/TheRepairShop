using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionBase : MonoBehaviour
{
    public static string RegionTag = "Region";
    public virtual void Awake()
    {
        gameObject.tag = RegionTag;
    }

    public virtual Item ItemInteraction(Item playerItem) { return null; }

}
