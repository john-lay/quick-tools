using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class highlightRadius : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;

    [Range(0, 5)]
    public float radius = 5;

    [Range(-10, 10)]
    public float offset;

    public Plane plane = Plane.xy;

    private LineRenderer line;

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    private void CreatePoints()
    {
        float a;
        float b;

        var angle = 20f;

        for (var i = 0; i < segments + 1; i++)
        {
            a = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            b = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            switch (plane)
            {
                case Plane.xy:
                    line.SetPosition(i, new Vector3(a, b, offset));
                    break;
                case Plane.yz:
                    line.SetPosition(i, new Vector3(offset, a, b));
                    break;
                case Plane.xz:
                    line.SetPosition(i, new Vector3(a, offset, b));
                    break;
            }


            angle += 360f / segments;
        }
    }
}

public enum Plane
{
    xy,
    yz,
    xz
}
