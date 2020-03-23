using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Towerdefense.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyData.asset", menuName = "Data/敌人配置", order = 1)]
    public class EnemyData : ScriptableObject
    {
        //名字
        public string name;

        //血量
        public float health;

        //魔抗(-1~1)
        public float magicResistance;

        //物抗(-1~1)
        public float physicalResistance;

        //速度
        public float speed;

        //掉落
        public int drop;
    }
}
