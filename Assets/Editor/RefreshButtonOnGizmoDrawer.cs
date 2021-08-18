using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GizmoBorderDrawer))]
public class RefreshButtonOnGizmoDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GizmoBorderDrawer gbd = (GizmoBorderDrawer)target;

        if (GUILayout.Button("Refresh"))
        {
            gbd.Refresh();
        }
    }
}
