using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GUIButton m_button;
    int m_ID;

    public Transform m_guardPrefab;

    public LayerMask m_groundLayer;
    public static GameManager Instance = null;
    public ArrayList m_enemyList = new ArrayList();

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

        m_button = this.transform.FindChild("button_0").GetComponent<GUIButton>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (m_life <= 0)
        {
            return;
        }
        bool press = Input.GetMouseButton(0);

        bool mouseup = Input.GetMouseButtonUp(0);
        Vector3 mousepos = Input.mousePosition;
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        if (m_ID > 0 && mouseup)
        {
            if (m_point < 5)
            {
                m_ID = 0;
                m_button.SetOnActive(false);
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, m_groundLayer))
            {
                int ix = (int)hit.point.x;
                int iz = (int)hit.point.z;

                if (iz >= GridMap.Instance.MapSizeX || iz >= GridMap.Instance.MapSizeZ||ix<0||iz<0)
                {
                    return;
                }
                if (GridMap.Instance.m_map[ix, iz].fieldType == MapData.FieldTypeID.GuardPosition)
                {
                    Vector3 pos = new Vector3((int)hit.point.x + 0.5f, 0, (int)hit.point.z + 0.5f);
                    Instantiate(m_guardPrefab, pos, Quaternion.identity);
                    m_ID = 0;
                    m_button.SetOnActive(false);
                    SetPoint(-5);
                }
            }
        }
        int id = m_button.UpdateState(mouseup, Input.mousePosition);
        if (id > 0)
        {
            m_ID = id;
            m_button.SetOnActive(true);
            return;
        }


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
