﻿using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {
    public PathNode m_parent;
    public PathNode m_next;

    public void SetNext(PathNode node)
    {
        if (m_next != null)
        {
            m_next.m_parent = null;
        }
        m_next = node;
        node.m_parent = this;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "Node.tif");
    }
}
