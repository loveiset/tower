using UnityEngine;
using System.Collections;

public class GridMap : MonoBehaviour {
    static public GridMap Instance = null;

    public bool m_debug = false;

    public int MapSizeX = 32;
    public int MapSizeZ = 32;

    public MapData[,] m_map;

    void Awake()
    {
        Instance = this;
        this.BuildMap();
    }

    [ContextMenu("BuildMap")]

    public void BuildMap()
    {
        m_map = new MapData[MapSizeX, MapSizeZ];
        for(int i=0;i<MapSizeX;i++)
        {
            for (int k = 0; k < MapSizeZ; k++)
            {
                m_map[i, k] = new MapData();
            }
        }

        GameObject[] nodes = (GameObject[])GameObject.FindGameObjectsWithTag("gridnode");
        foreach (GameObject nodeobj in nodes)
        {
            GridNode node = nodeobj.GetComponent<GridNode>();
            Vector3 pos = nodeobj.transform.position;

            if ((int)pos.x >= MapSizeX || (int)pos.z >= MapSizeZ)
            {
                continue;
            }
            m_map[(int)pos.x, (int)pos.z].fieldType = node._mapData.fieldType;
        }
    }

    void OnDrawGizmos()
    {
        if (!m_debug || m_map == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        float height = 0;
        for (int i = 0; i < MapSizeX; i++)
        {
            Gizmos.DrawLine(new Vector3(i, height, 0), new Vector3(i, height, MapSizeZ));
        }
        for (int k = 0; k < MapSizeZ; k++)
        {
            Gizmos.DrawLine(new Vector3(0, height, k), new Vector3(MapSizeX, height, k));
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < MapSizeX; i++)
        {
            for (int k = 0; k < MapSizeZ; k++)
            {
                if (m_map[i, k].fieldType == MapData.FieldTypeID.CanNotStand)
                {
                    Gizmos.color = new Color(1, 0, 0, 0.5f);
                    Gizmos.DrawCube(new Vector3(i + 0.5f, height, k + 0.5f), new Vector3(1, height + 0.1f, 1));
                }
            }
        }
    }

}
