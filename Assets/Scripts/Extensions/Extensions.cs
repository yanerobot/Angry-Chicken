using UnityEngine;

public static class Extensions
{
    public static Vector3 WhereX(this Vector3 vector, float x)
    {
        vector.x = x;
        return vector;
    }
    public static Vector3 WhereY(this Vector3 vector, float y)
    {
        vector.y = y;
        return vector;
    }
    public static Vector3 WhereZ(this Vector3 vector, float z)
    {
        vector.z = z;
        return vector;
    }

    public static Vector3 AddTo(this Vector3 vector, float x = 0, float y = 0, float z = 0)
    {
        vector.x += x;
        vector.y += y;
        vector.z += z;
        return vector;
    }
    public static Vector3 Multiply(this Vector3 vector, float x = 1, float y = 1, float z = 1)
    {
        vector.x *= x;
        vector.y *= y;
        vector.z *= z;
        return vector;
    }

    public static Vector2 WhereX(this Vector2 vector, float x)
    {
        vector.x = x;
        return vector;
    }
    public static Vector2 WhereY(this Vector2 vector, float y)
    {
        vector.y = y;
        return vector;
    }

    public static Vector2 AddTo(this Vector2 vector, float x = 0, float y = 0)
    {
        vector.x += x;
        vector.y += y;
        return vector;
    }
    public static Vector2 Multiply(this Vector2 vector, float x = 1, float y = 1)
    {
        vector.x *= x;
        vector.y *= y;
        return vector;
    }

    public static Vector2 Direction(this Transform t, Vector2 pos)
    {
        var heading = pos - (Vector2)t.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        return direction;
    }
}
