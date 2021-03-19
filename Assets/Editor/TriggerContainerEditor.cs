using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor (typeof(TriggerContainer))]
public class TriggerContainerEditor : Editor
{

    // private TrailRenderer _renderer;
    public AnimationCurve ac;
    private SerializedObject obj;

    public void OnEnable ()
    {
        
        obj = new SerializedObject (target);
    }
 
    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector ();
        EditorGUILayout.Space ();
        DropAreaGUI ();
        // OnGUI();
        DrawDragAndDrop(1);
    }
    
    void DrawDragAndDrop(int m){
 
        Event evt = Event.current;
 
        GUIStyle GuistyleBoxDND = new GUIStyle(GUI.skin.box);
        // GuistyleBoxDND.alignment = TextAnchor.$$anonymous$$iddleCenter;
        GuistyleBoxDND.fontStyle = FontStyle.Italic; 
        GuistyleBoxDND.fontSize = 12;
        GUI.skin.box = GuistyleBoxDND;
        // GuistyleBoxDND.normal.background = $$anonymous$$akeTex( 2, 2, Color.white);
 
        Rect dadRect = new Rect();
        dadRect = GUILayoutUtility.GetRect(0,20,GUILayout.ExpandWidth(true));
        GUI.Box(dadRect,"Drag and Drop Prefabs to this Box!",GuistyleBoxDND);
 
        if (dadRect.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.DragUpdated)
            {
                // DragAndDrop.visual$$anonymous$$ode = DragAndDropVisual$$anonymous$$ode.Copy;
                Debug.Log("Drag Updated!");
                Event.current.Use ();
            }   
            else if (Event.current.type == EventType.DragPerform)
            {
                Debug.Log("Drag Perform!");
                Debug.Log(DragAndDrop.objectReferences.Length);
                for(int i = 0; i<DragAndDrop.objectReferences.Length;i++)
                {
                    // myTarget.m_GameObjectGroups[m].Add(DragAndDrop.objectReferences[i] as GameObject);
                }
                Event.current.Use ();
            }
        }
    }
    public void DropAreaGUI ()
    {
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect (0.0f, 50.0f, GUILayout.ExpandWidth (true));
        GUI.Box (drop_area, "Add Trigger");
        // GUI.DragWindow();
        GUI.Button(new Rect(0.0f,0.0f,0.0f, 50.0f), "Create Node");
        if (GUILayout.Button("Create Node")) {
            // windows.Add(new Rect(10, 10, 100, 100));
            
            windows.Add(new Rect(10, 10, 100, 100));
        }
        
        switch (evt.type) {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                Debug.Log("drag performed");
                if (!drop_area.Contains (evt.mousePosition))
                    return;
             
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
         
                if (evt.type == EventType.DragPerform) {
                    DragAndDrop.AcceptDrag ();
             
                    foreach (Object dragged_object in DragAndDrop.objectReferences) {
                        // Do On Drag Stuff here
                        Debug.LogFormat("dropped = {0}", dragged_object);
                    }
                }
                break;
        }
    }

    List<Rect> windows = new List<Rect>();
    List<int> windowsToAttach = new List<int>();
    List<int> attachedWindows = new List<int>();
    void OnGUI() {
        if (windowsToAttach.Count == 2) {
            attachedWindows.Add(windowsToAttach[0]);
            attachedWindows.Add(windowsToAttach[1]);
            windowsToAttach = new List<int>();
        }
 
        if (attachedWindows.Count >= 2) {
            for (int i = 0; i < attachedWindows.Count; i += 2) {
                DrawNodeCurve(windows[attachedWindows[i]], windows[attachedWindows[i + 1]]);
            }
        }
 
        // BeginWindows();
 
        if (GUILayout.Button("Create Node")) {
            windows.Add(new Rect(10, 10, 100, 100));
        }
 
        for (int i = 0; i < windows.Count; i++) {
            windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, "Window " + i);
        }
 
        // EndWindows();
    }
 
 
    void DrawNodeWindow(int id) {
        if (GUILayout.Button("Attach")) {
            windowsToAttach.Add(id);
        }
 
        GUI.DragWindow();
    }
 
 
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
 
        for (int i = 0; i < 3; i++) {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
 
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

    // public void DropAreaGUI ()
    // {
    //     Event evt = Event.current;
    //     Rect drop_area = GUILayoutUtility.GetRect (0.0f, 50.0f, GUILayout.ExpandWidth (true));
    //     GUI.Box (drop_area, "Add Trigger");
    //     
    //     switch (evt.type) {
    //         case EventType.DragUpdated:
    //         case EventType.DragPerform:
    //             Debug.Log("drag performed");
    //             if (!drop_area.Contains (evt.mousePosition))
    //                 return;
    //          
    //             DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
    //      
    //             if (evt.type == EventType.DragPerform) {
    //                 DragAndDrop.AcceptDrag ();
    //          
    //                 foreach (Object dragged_object in DragAndDrop.objectReferences) {
    //                     // Do On Drag Stuff here
    //                     Debug.LogFormat("dropped = {0}", dragged_object);
    //                 }
    //             }
    //             break;
    //     }
    // }
}
