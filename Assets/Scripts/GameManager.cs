using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;

    [HideInInspector]
    public int m_wave = 1;

    public int m_life = 10;

    public int m_point = 10;

    GUIText m_text_wave;
    GUIText m_text_life;
    GUIText m_text_point;

    public bool m_debug = false;
    public ArrayList m_pathNodes;

    void Awake()
    {
        Instance = this;
    }

    [ContextMenu("BuildPath")]
    void BuildPath()
    {
        m_pathNodes = new ArrayList();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("pathnode");

        for (int i = 0; i < objs.Length; i++)
        {
            PathNode node = objs[i].GetComponent<PathNode>();
            m_pathNodes.Add(node);
        }

    }



	// Use this for initialization
	void Start () {
        m_text_life = this.transform.FindChild("text_life").GetComponent<GUIText>();
        m_text_wave = this.transform.FindChild("text_wave").GetComponent<GUIText>();
        m_text_point = this.transform.FindChild("text_point").GetComponent<GUIText>();

        m_text_wave.text = "<color=red>wave</color>" + m_wave;
        m_text_life.text = "<color=red>life</color>" + m_life;
        m_text_point.text = "<color=red>point</color>" + m_point;
	
	}
	
	// Update is called once per frame
	void Update () {
        bool press = Input.GetMouseButton(0);
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        GameCamera.Instance.Control(press, mx, my);
	
	}

    public void SetWave(int wave)
    {
        m_wave = wave;
        m_text_wave.text = "<color=red>wave</color>" + m_wave;
    }

    public void SetDamage(int damage)
    {
        m_life -= damage;
        m_text_life.text = "<color=red>life</color>" + m_life;
    }
    public void SetPoint(int point)
    {
        m_point += point;
        m_text_point.text = "<color=red>point</color>" + m_point;
    }

    void OnDrawGizmos()
    {
        if (!m_debug || m_pathNodes == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        foreach (PathNode node in m_pathNodes)
        {
            if (node.m_next != null)
            {
                Gizmos.DrawLine(node.transform.position, node.m_next.transform.position);
            }
        }
    }
}
