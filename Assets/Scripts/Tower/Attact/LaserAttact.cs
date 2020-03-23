using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towerdefense.Tower;
using Towerdefense.Tower.Data;


public class LaserAttact : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();//范围内敌人

    public Transform head;

    private float laserDamage = 70.0f;
    private float attackRate;
    private float explosionRange;

    public LineRenderer laserRenderer;

    public GameObject laserEffect;

    private float m_Timer;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            int index = enemys.IndexOf(col.gameObject);
            if (gameObject.name == "MagicTower Ice(Clone)")
            {
                enemys[index].GetComponent<Enemy>().speedRate = 1f;
            }
            enemys.Remove(col.gameObject);
        }
    }
    //private float attactRateTime = 1.0f;//多少秒攻击一次
    //private float timer = 0.0f;


    //public GameObject bulletPrefab;//子弹
    public Transform firePosition;//开火位置
    private void Start()
    {
        //attactRateTime = GetComponent<TowerLevel>().attackRate;
        laserDamage = GetComponent<TowerLevel>().attackPower;
        GetComponent<SphereCollider>().radius = GetComponent<TowerLevel>().range;
        attackRate = GetComponent<TowerLevel>().attackRate;
        explosionRange = GetComponent<TowerLevel>().explosionRange;
        m_Timer = 0;

    }
    private void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        
        if (enemys.Count > 0)
        {
            if(laserRenderer.enabled==false)
            {
                laserRenderer.enabled = true;
                laserEffect.SetActive(true);
            }
            if(enemys[0]==null)
            {
                UpdateEnemys();
            }
            if(enemys.Count>0)
            {
                int vecLong;
                vecLong = (int)explosionRange > enemys.Count ? enemys.Count : (int)explosionRange;
                laserRenderer.positionCount = vecLong+1;
                Vector3[] vertexs = new Vector3[vecLong+1];
                vertexs[0] = firePosition.position;
                //laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                for(int i=0;i< explosionRange && i < enemys.Count;i++)
                {
                    Vector3 temp = enemys[i].transform.position;
                    temp.y += 2.5f;
                    vertexs[i + 1] = temp;
                }
                laserRenderer.SetPositions(vertexs);
                laserEffect.transform.position = enemys[0].transform.position;
                for (int i=0;i< explosionRange && i<enemys.Count;i++)
                {
                    enemys[i].GetComponent<Enemy>().TakeDamage((laserDamage/ attackRate) * Time.deltaTime * (1 - i * 0.1f), GetComponent<TowerLevel>().isMagicDamage);
                    if(gameObject.name=="MagicTower Ice(Clone)")
                    {
                        enemys[i].GetComponent<Enemy>().speedRate = 0.5f;
                    }
                }
                Vector3 position = transform.position;
                position.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(position);
                m_Timer += Time.deltaTime;
                if (m_Timer > attackRate)
                {
                    laserRenderer.enabled = false;
                    m_Timer = 0f;
                }
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }

    void Attact()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            //GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            //bullet.GetComponent<Bullet>().SetDamage(GetComponent<TowerLevel>().attackPower, GetComponent<TowerLevel>().isMagicDamage);
            //bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            //timer = attactRateTime;
        }

    }

    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
