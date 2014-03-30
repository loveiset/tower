using UnityEngine;
using UnityEditor;
using System.Collections;

public class PathTool : ScriptableObject {
    static PathNode m_parent = null;

    [MenuItem("PathTool/Set Parent %q")]
    static void SetParent()
    {
        if (!Selection.activeGameObject || Selection.GetTransforms(SelectionMode.Unfiltered).Length > 1)
        {
            return;
        }
        if (Selection.activeGameObject.tag.CompareTo("pathnode") == 0)
        {
            m_parent = Selection.activeGameObject.GetComponent<PathNode>();
        }
    }
    [MenuItem("PathTool/Set Next %w")]
    static void SetNextChild()
    {
        if (!Selection.activeGameObject || m_parent == null || Selection.GetTransforms(SelectionMode.Unfiltered).Length > 1)
        {
            return;
        }
        if (Selection.activeGameObject.tag.CompareTo("pathnode") == 0)
        {
            m_parent.SetNext(Selection.activeGameObject.GetComponent<PathNode>());
            m_parent = null;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
