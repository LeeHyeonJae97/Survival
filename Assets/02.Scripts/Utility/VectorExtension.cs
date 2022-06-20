using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 SetX(this Vector2 vec, float x)
    {
        return new Vector2(x, vec.y);
    }

    public static Vector2 SetY(this Vector2 vec, float y)
    {
        return new Vector2(vec.x, y);
    }

    public static Vector3 SetX(this Vector3 vec, float x)
    {
        return new Vector3(x, vec.y, vec.z);
    }

    public static Vector3 SetY(this Vector3 vec, float y)
    {
        return new Vector3(vec.x, y, vec.z);
    }

    public static Vector3 SetZ(this Vector3 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    public static Vector2Int SetX(this Vector2Int vec, int x)
    {
        return new Vector2Int(x, vec.y);
    }

    public static Vector2Int SetY(this Vector2Int vec, int y)
    {
        return new Vector2Int(vec.x, y);
    }

    public static Vector3Int SetX(this Vector3Int vec, int x)
    {
        return new Vector3Int(x, vec.y, vec.z);
    }

    public static Vector3Int SetY(this Vector3Int vec, int y)
    {
        return new Vector3Int(vec.x, y, vec.z);
    }

    public static Vector3Int SetZ(this Vector3Int vec, int z)
    {
        return new Vector3Int(vec.x, vec.y, z);
    }

    public static Vector2Int ToInt(this Vector2 vec)
    {
        return new Vector2Int((int)vec.x, (int)vec.y);
    }

    public static Vector3Int ToInt(this Vector3 vec)
    {
        return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
    }

    public static Vector2 Round(this Vector2 vec)
    {
        return new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
    }

    public static Vector3 Round(this Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
    }

    public static Vector2 Ceil(this Vector2 vec)
    {
        return new Vector2(Mathf.Ceil(vec.x), Mathf.Ceil(vec.y));
    }

    public static Vector3 Ceil(this Vector3 vec)
    {
        return new Vector3(Mathf.Ceil(vec.x), Mathf.Ceil(vec.y), Mathf.Ceil(vec.z));
    }

    public static Vector2 Floor(this Vector2 vec)
    {
        return new Vector2(Mathf.Floor(vec.x), Mathf.Floor(vec.y));
    }

    public static Vector3 Floor(this Vector3 vec)
    {
        return new Vector3(Mathf.Floor(vec.x), Mathf.Floor(vec.y), Mathf.Floor(vec.z));
    }

    public static Vector3 XYToXZ(this Vector2 vec)
    {
        return new Vector3(vec.x, 0, vec.y);
    }

    public static Vector2 XZToXY(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }

    public static Vector2 Divide(this Vector2 vec)
    {
        Vector2 base1 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * 22.5f), Mathf.Sin(Mathf.Deg2Rad * 22.5f));
        Vector2 base2 = new Vector2(Mathf.Cos(Mathf.Deg2Rad * 67.5f), Mathf.Sin(Mathf.Deg2Rad * 67.5f));

        if (-base2.x < vec.x && vec.x <= base2.x)
        {
            return vec.y > 0 ? Vector2.up : Vector2.down;
        }
        else if (base2.x < vec.x && vec.x <= base1.x)
        {
            return vec.y > 0 ? new Vector2(1, 1) : new Vector2(1, -1);
        }
        else if (-base1.x < vec.x && vec.x <= -base2.x)
        {
            return vec.y > 0 ? new Vector2(-1, 1) : new Vector2(-1, -1);
        }
        else if (vec.x <= -base1.x)
        {
            return Vector2.left;
        }
        else if (base1.x < vec.x)
        {
            return Vector2.right;
        }
        else
        {
            Debug.LogError("Error");
            return Vector2.zero;
        }
    }

    public static Vector3 DivideXZ(this Vector3 vec)
    {
        Vector3 base1 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 22.5f), 0, Mathf.Sin(Mathf.Deg2Rad * 22.5f));
        Vector3 base2 = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 67.5f), 0, Mathf.Sin(Mathf.Deg2Rad * 67.5f));

        if (-base2.x < vec.x && vec.x <= base2.x)
        {
            return vec.z > 0 ? Vector3.forward : Vector3.back;
        }
        else if (base2.x < vec.x && vec.x <= base1.x)
        {
            return vec.z > 0 ? new Vector3(1, 0, 1) : new Vector3(1, 0, -1);
        }
        else if (-base1.x < vec.x && vec.x <= -base2.x)
        {
            return vec.z > 0 ? new Vector3(-1, 0, 1) : new Vector3(-1, 0, -1);
        }
        else if (vec.x <= -base1.x)
        {
            return Vector3.left;
        }
        else if (base1.x < vec.x)
        {
            return Vector3.right;
        }
        else
        {
            Debug.LogError("Error");
            return Vector3.zero;
        }
    }

    public static float Dst(this Vector2 from, Vector2 to)
    {
        return (from - to).magnitude;
    }

    public static float Dst(this Vector3 from, Vector3 to)
    {
        return (from - to).magnitude;
    }

    public static float SqrDst(this Vector2 from, Vector2 to)
    {
        return (from - to).sqrMagnitude;
    }

    public static float SqrDst(this Vector3 from, Vector3 to)
    {
        return (from - to).sqrMagnitude;
    }
}
