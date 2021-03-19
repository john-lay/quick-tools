using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineCurve))]
public class LineCurveEditor : Editor
{
    private SerializedObject obj;
    private Rect curveCanvas;
    Vector2 mousePosition = Vector2.zero;

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
        // DrawAc();
    }

    void DrawDragPoint()
    {
        // var p11 = new Vector2(200, 200);
        // var p21 = new Vector2(210, 210);
        // Handles.DrawLine(p11, p21);
        
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            var p1 = Event.current.mousePosition;
            var p2 = p1 + new Vector2(10, 10);
            Debug.LogFormat("Left-Mouse Down {0}, {1}", p1, p2);
            
            // Rect rect = GUILayoutUtility.GetRect(10, 1000, 200, 200);
            // Handles.DrawLine(p1, p2);
            // Repaint();
            
            // var p11 = new Vector2(200, 200);
            // var p21 = new Vector2(210, 210);
            // Handles.DrawLine(p11, p21);
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
            Debug.Log("repainting");
            mousePosition = Vector2.zero;
        }

        var btn = GUI.Button(new Rect(curveCanvas.x + 20, curveCanvas.y + 20, 20, 20), GUIContent.none);
        if (curveCanvas.Contains(Event.current.mousePosition))
        {
            if (btn)
            {
            }

            // if (Event.current.type == EventType.DragUpdated)
            // {
            //     DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            //     Debug.Log("Drag Updated!");
            //     Event.current.Use();
            // }
        }

        // GUI.DragWindow();
        
        
    }

    void DrawMyLine(string label)
    {
        // draw a label       
        EditorGUILayout.LabelField(label);
        
        // draw a canvas
        Rect labelRect = GUILayoutUtility.GetLastRect();
        curveCanvas = new Rect(labelRect.x, labelRect.y + labelRect.height, labelRect.width, 180);
        DebugRect(curveCanvas);
        
        var start = new Vector2(curveCanvas.x + 10, curveCanvas.y + 20);
        var end = new Vector2(curveCanvas.x + 100,  curveCanvas.y + 100);
        // Debug.LogFormat("start {0}, end {1}", start, end);
        Handles.DrawLine(start, end);
    }

    void DrawAc()
    {
        EditorGUILayout.LabelField("my label");
        var ac = new AnimationCurve();
        EditorGUILayout.CurveField(ac);
    }

    void DebugRect(Rect rect)
    {
        var imgPath = "Icons/square";
        Texture2D BoxBorder = Resources.Load<Texture2D>(imgPath);
        var borderSize = 2; // Border size in pixels
        // var style = new GUIStyle();
        // style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);
        // style.normal.background = BoxBorder;
        var style = new GUIStyle(GUI.skin.box);
        
        GUI.Box(rect, GUIContent.none, new GUIStyle(style));
    }

}
