using UnityEngine;

public class CameraBoundPoints : MonoBehaviour
{

    [SerializeField]
    private float left;
    [SerializeField]
    private float right;
    [SerializeField]
    private float top;
    [SerializeField]
    private float bottom;

    public float GetLeft()
    {
        return left;
    }

    public float GetRight()
    {
        return right;
    }

    public float GetTop()
    {
        return top;
    }

    public float GetBottom()
    {
        return bottom;
    }

}