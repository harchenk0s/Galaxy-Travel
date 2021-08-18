using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BorderPoint))]
public class GizmosBorderDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BorderPoint pb = (BorderPoint)target;

        if (GUILayout.Button("Refresh"))
        {
            pb.Refresh();
        }
    }
}
