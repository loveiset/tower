using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
    public float m_currentLife = 1.0f;

    public float m_maxLife = 1.0f;

    internal Transform m_transform;

    float m_hscale = 1.0f;
    float m_vscale = 1.0f;

    Mesh m_mesh;

    Transform m_cameraTransform;

    Vector2[] m_Uvs;

    public void Ini(float currentLife, float maxLife, float hscale, float vscale)
    {
        m_transform = this.transform;
        m_cameraTransform = Camera.main.transform;

        m_hscale = hscale;
        m_vscale = vscale;
        m_transform.localScale = new Vector3(hscale, vscale, 1.0f);

        m_mesh = (Mesh)this.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = m_mesh.vertices;

        m_Uvs = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            m_Uvs[i] = m_mesh.uv[i];
        }
        UpdateLife(currentLife, maxLife);
    }

    void Pad(float value)
    {
        float left = (1.0f - value) / 2 + 0.0f;
        float right = 0.5f + (1.0f - value) / 2 - 0.01f;

        m_Uvs[0] = new Vector2(left, 0.0f);
        m_Uvs[3] = new Vector2(left, 0.1f);

        m_Uvs[1] = new Vector2(right, 0.0f);
        m_Uvs[2] = new Vector2(right, 0.1f);

        m_mesh.uv = m_Uvs;
    }

    public void UpdateLife(float currentLife, float maxLife)
    {
        if (maxLife == 0)
        {
            return;
        }
        m_currentLife = currentLife;
        m_maxLife = maxLife;
        this.Pad(currentLife / maxLife);

        m_transform.localScale = new Vector3(m_hscale, m_vscale, 1.0f);
    }

    public void SetPosition(Vector3 position, float yoffset)
    {
        Vector3 vec = position;
        vec.y += yoffset;
        m_transform.position = vec;

        Vector3 rot = new Vector3();
        rot.y = m_cameraTransform.eulerAngles.y;
        rot.x = m_cameraTransform.eulerAngles.x;

        m_transform.eulerAngles = rot;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
