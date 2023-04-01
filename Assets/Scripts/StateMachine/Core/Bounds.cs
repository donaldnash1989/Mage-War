using UnityEngine;

[System.Serializable]
public struct Bounds
{
    public float Top;
    public float Right;
    public float Bottom;
    public float Left;

    public Bounds(float top, float right, float bottom, float left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }

    public bool IsInBounds(Vector3 position)
    {
        if (position.x <= Left || position.x >= Right || position.y <= Bottom || position.y >= Top) return false;
        return true;
    }
}


