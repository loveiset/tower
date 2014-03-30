﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public EnemySpawner m_spawn;
    public PathNode m_currentNode;

    public int m_life = 15;
    public int m_maxLife = 15;
    public float m_speed = 2;

    public enum TYPE_ID
    {
        GROUND,
        AIR,
    }
    public TYPE_ID m_type = TYPE_ID.GROUND;


	// Use this for initialization
	void Start () {
        m_spawn.m_liveEnemy++;
    }

    void OnDisable()
    {
        if (m_spawn)
        {
            m_spawn.m_liveEnemy--;
        }
    }
	
	// Update is called once per frame
	void Update () {
        RotateTo();
        MoveTo();
	
	}
    public void RotateTo()
    {
        float current = this.transform.eulerAngles.y;
        this.transform.LookAt(m_currentNode.transform);
        Vector3 target = this.transform.eulerAngles;
        float next = Mathf.MoveTowardsAngle(current, target.y, 120 * Time.deltaTime);

        this.transform.eulerAngles = new Vector3(0, next, 0);
    }
    public void MoveTo()
    {
        Vector3 pos1 = this.transform.position;
        Vector3 pos2 = m_currentNode.transform.position;

        float dist = Vector2.Distance(new Vector2(pos1.x, pos1.z), new Vector2(pos2.x, pos2.z));
        if (dist < 1.0f)
        {
            if (m_currentNode.m_next == null)
            {
                GameManager.Instance.SetDamage(1);
                Destroy(this.gameObject);
            }
            else
            {
                m_currentNode = m_currentNode.m_next;
            }

        }
        

        this.transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }
}
