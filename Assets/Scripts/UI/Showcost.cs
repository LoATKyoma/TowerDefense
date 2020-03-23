using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towerdefense.Tower.Data;
using UnityEngine.UI;

public class Showcost : MonoBehaviour
{
    public TowerLevelData data;
    private float m_Cost { get { return data.cost; } }

    void Start()
    {
        gameObject.GetComponent<Text>().text = "花费:" + m_Cost.ToString();
        gameObject.GetComponent<Text>().fontSize = 22;
    }
}
