using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towerdefense.Tower.Data;

namespace Towerdefense.Tower
{
    public class TowerLevel : MonoBehaviour
    {
        public string name;
        public string description;
        public TowerLevelData levelData;
        public GameObject towerPrefab;

        //塔的等级
        public int level;


        //防御塔配置
        //string m_Name { get { return levelData.name; } }
        [HideInInspector]
        public float attackPower { get { return levelData.attackPower; } }
        public float range { get { return levelData.Range; } }
        public float attackRate { get { return levelData.attackRate; } }
        public bool isMagicDamage { get { return levelData.isMagicDamage; } }
        public int cost { get { return levelData.cost; } }
        public TowerLevelData.TowerTypes towerType { get { return levelData.towerType; } }
        public float explosionRange { get { return levelData.explosionRange; } }
    }
}


