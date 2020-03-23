using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towerdefense.Enemy.Data;
using UnityEngine.UI;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public EnemyData enemyConfiguration;

    public float hp = 100;
    private float totalHp;

    private Slider hpSlider;

    public GameObject EnemyDieEffect;

    public float turnSpeed = 30;

    [HideInInspector]
    public float speedRate = 1f;

    private Animator animator;

    public bool isBoss = false;

    //敌人的配置
    string m_Name { get { return enemyConfiguration.name; } }
    float m_Speed { get { return enemyConfiguration.speed; } }
    float m_MagicResistance { get { return enemyConfiguration.magicResistance; } }
    float m_PhysicalResistance { get { return enemyConfiguration.physicalResistance; } }
    float m_MaxHealth { get { return enemyConfiguration.health; } }
    int m_Drop { get { return enemyConfiguration.drop; } }

    private int m_Index = 1;
    private Vector3 m_Vec3;
    private Transform[] node;
    private Vector3 m_CalVec3;

    void Start()
    {
        animator = GetComponent<Animator>();
        hp = m_MaxHealth;
        node = Node.nodePosition;
        hpSlider = GetComponentInChildren<Slider>();
        totalHp = hp;
        m_Vec3 = (node[m_Index].position - transform.position).normalized;

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        //转向和移动
        transform.Translate(m_Vec3 * m_Speed * speedRate * Time.deltaTime, Space.World);
        Rotate(m_Vec3);
        //寻找下一个路径点
        if ((m_Index < node.Length) && (Vector3.Distance(transform.position, node[m_Index].position) < 1.5f))
        {
            m_Index++;
            m_Vec3 = (node[m_Index].position - transform.position).normalized;
        }
        //到终点销毁
        if (m_Index > node.Length - 1)
        {
            ReachEnd();
            OnDestory();
        }
    }

    void ReachEnd()
    {
        GameObject.Destroy(this.gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().playerHealth--;
    }

    void Rotate(Vector3 direction)
    {
        Quaternion qua = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, qua, Time.deltaTime * turnSpeed);
    }

    void OnDestory()
    {
        //摧毁时存活敌人数量减少
        EnemySpawnManager.aliveEnemyCount--;
    }

    public void TakeDamage(float damage, bool ismagic)
    {
        if (hp <= 0) return;
        if (ismagic)
        {
            hp -= damage * (1 - (m_MagicResistance / 100));
        }
        else
        {
            hp -= damage * (1 - (m_PhysicalResistance / 100));
        }
        hpSlider.value = hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsAlive", false);
        GameObject effect = Instantiate(EnemyDieEffect, transform.position, transform.rotation);
        Destroy(effect, 2.0f);
        if (isBoss)
        {
            Destroy(this.gameObject, 2.0f);
        }
        else
        { Destroy(this.gameObject); }

        GameObject.Find("GameManager").GetComponent<BuildManager>().money += m_Drop;
        OnDestory();
    }
}
