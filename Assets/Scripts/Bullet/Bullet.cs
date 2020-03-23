using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public List<GameObject> enemyInRange = new List<GameObject>();//爆炸范围内敌人

    private float exploationRange;

    public float damage = 50;

    public float speed = 20.0f;

    public bool isMagic = false;

    private Transform target;

    public GameObject explosionEffectPrefab;

    public float distanceToTarget = 1.0f;

    private Vector3 saveTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            enemyInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Enemy")
        {
            enemyInRange.Remove(other.gameObject);
        }
    }

    private void Start()
    {
        GetComponent<SphereCollider>().radius = exploationRange;
        saveTarget = target.position;
    }

    public void SetExpolsionRange(float range)
    {
        exploationRange = range;
    }

    public void SetDamage(float attactpower,bool ismagicdamage)
    {
        damage = attactpower;
        isMagic = ismagicdamage;
    }

    

    public void SetTarget(Transform _target)
    {
        this.target = _target;
        saveTarget = target.position;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            Vector3 dir = target.position - transform.position;
            if (dir.magnitude < distanceToTarget)
            {
                target.GetComponent<Enemy>().TakeDamage(damage, isMagic);
                for (int i = 0; i < enemyInRange.Count; i++)
                {
                    if (enemyInRange[i] == null || enemyInRange[i].transform == target) continue;
                    else
                    {
                        Vector3 distenceTo = enemyInRange[i].transform.position - target.position;
                        enemyInRange[i].GetComponent<Enemy>().TakeDamage(CalDistanceDamage(distenceTo.magnitude) * damage, isMagic);
                    }
                }
                Die();
            }
            saveTarget = target.position;
        }
        else
        {
            transform.LookAt(saveTarget);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Vector3 dir = saveTarget - transform.position;
            if(dir.magnitude<distanceToTarget)
            {
                for(int i = 0; i < enemyInRange.Count; i++)
                {
                    if (enemyInRange[i] == null) continue;
                    else
                    {
                        Vector3 distenceTo = enemyInRange[i].transform.position - saveTarget;
                        enemyInRange[i].GetComponent<Enemy>().TakeDamage(CalDistanceDamage(distenceTo.magnitude) * damage, isMagic);
                    }
                    Die();
                }
            }

        }
    }

    void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Vector3 point = new Vector3(0, 1000, 0);
        effect.transform.LookAt(point);
        Destroy(effect, 2);
        Destroy(this.gameObject);
    }

    float CalDistanceDamage(float dis)
    {
        return exploationRange / (exploationRange + dis);
    }
}
