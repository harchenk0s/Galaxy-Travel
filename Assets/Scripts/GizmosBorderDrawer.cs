using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointerBorder))]
public class GizmosBorderDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PointerBorder pb = (PointerBorder)target;

        if (GUILayout.Button("Refresh"))
        {
            pb.Refresh();
        }
    }

    
}
