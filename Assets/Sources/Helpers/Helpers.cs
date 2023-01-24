using UnityEngine;

public static class Helpers
{
    #region Position
    
    public static void SetPosX(this Transform t, float x)
    {
        var pos = t.position;
        pos = new Vector3(x, pos.y, pos.z);
        t.position = pos;
    }

    public static void SetPosY(this Transform t, float y)
    {
        var pos = t.position;
        pos = new Vector3(pos.x, y, pos.z);
        t.position = pos;
    }

    public static void SetPosZ(this Transform t, float z)
    {
        var pos = t.position;
        pos = new Vector3(pos.x, pos.y, z);
        t.position = pos;
    }

    public static void SetPosXY(this Transform t, float x, float y)
    {
        var pos = t.position;
        pos = new Vector3(x, y, pos.z);
        t.position = pos;
    }

    public static void SetPosXZ(this Transform t, float x, float z)
    {
        var pos = t.position;
        pos = new Vector3(x, pos.y, z);
        t.position = pos;
    }

    public static void SetPosYZ(this Transform t, float y, float z)
    {
        var pos = t.position;
        pos = new Vector3(pos.x, y, z);
        t.position = pos;
    }
    
    #endregion

    #region Scale

    public static void SetScaleX(this Transform t, float scaleX)
    {
        var scale = t.localScale;
        scale = new Vector3(scaleX, scale.y, scale.z);
        t.localScale = scale;
    }

    public static void SetScale(this Transform t, float s)
    {
        var scale = t.localScale;
        scale = new Vector3(s, s, s);
        t.localScale = scale;
    } 

    #endregion
}
