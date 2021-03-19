using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineCurve))]
public class LineCurveEditor : Editor
{
    private SerializedObject obj;
    private Rect curveCanvas;
    private Vector2 mousePosition = Vector2.zero;
    private float minSliderValue;
    private float maxSliderValue;
    private Texture2D BoxBorder;

    public void OnEnable()
    {
        // obj = new SerializedObject(target);
    }

    // called everytime the inspector is drawn
    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        LineCurve lineCurve = (LineCurve)target;
        EditorGUILayout.Space();
        lineCurve.experience = EditorGUILayout.IntField("Expereince", lineCurve.experience);
        EditorGUILayout.LabelField("Level", lineCurve.level.ToString());
        
        /////////
        // EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.box), GUILayout.Height(200));
        EditorGUILayout.BeginVertical(GUIStyle.none, GUILayout.Height(200));
        
        DrawMyLine("Curve");
        DrawDragPoint();
        EditorGUILayout.EndVertical();
        
        ////////
        // draw a max value slider
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        maxSliderValue = EditorGUILayout.Slider(maxSliderValue, 0, 1);
        
    }

    void DrawDragPoint()
    {
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            // var p1 = Event.current.mousePosition;
            // var p2 = p1 + new Vector2(10, 10);
            // Debug.LogFormat("Left-Mouse Down {0}, {1}", p1, p2);
            mousePosition = Event.current.mousePosition;
            Repaint();

        }

        if (Event.current.type == EventType.Repaint && mousePosition != Vector2.zero)
        {
            var p1 = mousePosition;
            var p2 =  p1 + new Vector2(10, 10);
            var p3 = p1 + new Vector2(10, 0);
            var p4 = p1 + new Vector2(0, 10);
            Handles.DrawLine(p1, p2);
            Handles.DrawLine(p3, p4);
            // Debug.Log("repainting");
            // mousePosition = Vector2.zero;
        }
    }

    void DrawMyLine(string label)
    {
        // draw a label       
        EditorGUILayout.LabelField(label);
        
        // draw a min value slider
        minSliderValue = EditorGUILayout.Slider(minSliderValue, 0, 1);
        
        // draw a canvas
        Rect labelRect = GUILayoutUtility.GetLastRect();
        curveCanvas = new Rect(labelRect.x, labelRect.y + labelRect.height, labelRect.width, 180);
        DebugRect(curveCanvas);
        
        // var start = new Vector2(curveCanvas.x + 10, curveCanvas.y + 20);
        // var end = new Vector2(curveCanvas.x + 100,  curveCanvas.y + 100);
        // // Debug.LogFormat("start {0}, end {1}", start, end);
        // Handles.DrawLine(start, end);
        
        // draw a bezier curve
        var minPosition = curveCanvas.width * maxSliderValue + 20;
        var startPosition = new Vector2(minPosition, curveCanvas.y + 170);
        
        var maxPosition = curveCanvas.width * minSliderValue + 20;
        var endPosition = new Vector2(maxPosition,  curveCanvas.y + 10);
        
        // var startTangent = new Vector2(curveCanvas.x + 10, curveCanvas.y + 20);
        // var startTangent = Vector3.zero;
        var startTangent = mousePosition;
        // var endTangent = new Vector2(curveCanvas.x + 100,  curveCanvas.y + 100);
        // var endTangent = Vector3.zero;
        var endTangent = mousePosition;
        
        // Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, Color.black, BoxBorder, 2);
        Handles.DrawBezier(startPosition, endPosition, startTangent, endTangent, Color.black, BoxBorder, 2);
    }

    void DebugRect(Rect rect)
    {
        var imgPath = "Icons/square";
        // Texture2D BoxBorder = Resources.Load<Texture2D>(imgPath);
        BoxBorder = Resources.Load<Texture2D>(imgPath);
        var borderSize = 2; // Border size in pixels
        // var style = new GUIStyle();
        // style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);
        // style.normal.background = BoxBorder;
        var style = new GUIStyle(GUI.skin.box);
        
        GUI.Box(rect, GUIContent.none, new GUIStyle(style));
    }

}
