﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
    public float damage = 50;

    public float speed = 20.0f;

    public bool isMagic = false;

    private Transform target;

    public GameObject explosionEffectPrefab;

    public float distanceToTarget = 1.0f;

    public void SetDamage(float attactpower, bool ismagicdamage)
    {
        damage = attactpower;
        isMagic = ismagicdamage;
    }



    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceToTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage, isMagic);
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
