using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SquareArea : MonoBehaviour
{
    [SerializeField] private Vector2 _squareSize;
    public Vector2 GetRandomPointInSquare()
    {
        Vector2 pos = new Vector2(Random.Range(transform.position.x - _squareSize.x, transform.position.x + _squareSize.x),
            Random.Range(transform.position.y - _squareSize.y, transform.position.y + _squareSize.y));
        return pos;
    }

#if UNITY_EDITOR
    private static Color GizmoColor = new Color(1, 0, 0, 0.3f);
    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Vector3 point1 = transform.position + (Vector3)_squareSize;
        Vector3 point2 = new Vector3(point1.x, point1.y - _squareSize.y * 2, 0);
        Vector3 point3 = transform.position - (Vector3)_squareSize;
        Vector3 point4 = new Vector3(point3.x, point3.y + _squareSize.y * 2, 0);
        Handles.DrawPolyLine(point1, point2, point3, point4, point1);
        
    }
#endif
}
