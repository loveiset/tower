﻿using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {
    Enemy m_targetEnemy;
    public float m_attackArea = 4.0f;

    public int m_power = 5;

    public float m_attackTime = 1.0f;

    public float m_timer = 0.0f;

	// Use this for initialization
	void Start () {
        GridMap.Instance.m_map[(int)this.transform.position.x, (int)this.transform.position.z].fieldType = MapData.FieldTypeID.CanNotStand;

	
	}
	
	// Update is called once per frame
	void Update () {
        FindEnemy();
        RotateTo();
        Attack();
	
	}
    public void RotateTo()
    {
        if (m_targetEnemy == null)
        {
            return;
        }
        Vector3 current = this.transform.eulerAngles;
        this.transform.LookAt(m_targetEnemy.transform);

        Vector3 target = this.transform.eulerAngles;

        float next = Mathf.MoveTowards(current.y, target.y, 120 * Time.deltaTime);
        this.transform.eulerAngles = new Vector3(current.x, next, current.z);
    }
    public void FindEnemy()
    {
        m_targetEnemy = null;
        int lastLife = 0;
        foreach (Enemy enemy in GameManager.Instance.m_enemyList)
        {
            if (enemy.m_life <= 0)
            {
                return;
            }
            Vector3 pos1 = this.transform.position;
            Vector3 pos2 = enemy.transform.position;

            float dist = Vector2.Distance(new Vector2(pos1.x, pos1.z), new Vector2(pos2.x, pos2.z));

            if (dist < m_attackArea)
            {
                continue;
            }
            if (lastLife == 0 || lastLife > enemy.m_life)
            {
                m_targetEnemy = enemy;
                lastLife = enemy.m_life;
            }
        }


    }
    public void Attack()
    {
        m_timer -= Time.deltaTime;
        if (m_targetEnemy == null)
        {
            return;
        }
        if (m_timer > 0)
        {
            return;
        }
        m_targetEnemy.SetDamage(m_power);
        Debug.Log(m_power);
        m_timer = m_attackTime;
    }
   

}
