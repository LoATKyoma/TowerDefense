using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Towerdefense.Tower.Data
{
    //创建菜单，用于生成data asset
    [CreateAssetMenu(fileName = "TowerData.asset", menuName = "Data/防御塔配置", order = 1)]
    public class TowerLevelData : ScriptableObject
    {
        public enum TowerTypes
        {
            Arrow,
            Magic,
            Cannon
        };
        //塔名
        //public string name;

        //攻击力
        public float attackPower;

        //射速
        public float attackRate;

        //射程
        public float Range;

        //伤害类型
        public bool isMagicDamage;

        //花费
        public int cost;

        //塔的类型
        public TowerTypes towerType;

        //爆炸半径
        public float explosionRange;
    }
}
