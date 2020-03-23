using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Transform[] nodePosition;

    void Awake()
    {
        nodePosition = new Transform[transform.childCount];
        for(int i = 0; i<nodePosition.Length; i++)
        {
            nodePosition[i] = transform.GetChild(i);
        }
    }
}
