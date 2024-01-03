using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckets : MonoBehaviour
{
    private int val = 0;
    void OnDestroy()
    {
        Brain.juice += Juice.maxjuice - Mathf.Max(Brain.juice, Juice.maxjuice - 10);
        Well.isBlob[val] = false;
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }

    public void setVal(int x)
    {
        val = x;
    }
}
