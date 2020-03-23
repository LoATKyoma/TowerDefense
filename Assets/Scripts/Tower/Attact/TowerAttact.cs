using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towerdefense.Tower;

public class TowerAttact : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();//范围内敌人

    public Transform head;

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
            enemys.Remove(col.gameObject);
        }
    }
    private float attactRateTime = 1.0f;//多少秒攻击一次
    private float timer = 0.0f;


    public GameObject bulletPrefab;//子弹
    public Transform firePosition;//开火位置
    private void Start()
    {
        attactRateTime = GetComponent<TowerLevel>().attackRate;

        GetComponent<SphereCollider>().radius = GetComponent<TowerLevel>().range;

        timer = attactRateTime;


    }
    private void Update()
    {
        if (enemys.Count>0&&enemys[0]!=null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attactRateTime)
        {
            timer = 0.0f;
            Attact();
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
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetDamage(GetComponent<TowerLevel>().attackPower, GetComponent<TowerLevel>().isMagicDamage);
            bullet.GetComponent<Bullet>().SetExpolsionRange(GetComponent<TowerLevel>().explosionRange);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attactRateTime;
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
