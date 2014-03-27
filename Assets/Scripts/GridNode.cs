using UnityEngine;
using System.Collections;
[System.Serializable]
public class MapData
{
    public enum FieldTypeID
    {
        GuardPosition,
        CanNotStand,
    }
    public FieldTypeID fieldType = FieldTypeID.GuardPosition;
}

public class GridNode : MonoBehaviour {
    public MapData _mapData;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "gridnode.tif");
    }


}
