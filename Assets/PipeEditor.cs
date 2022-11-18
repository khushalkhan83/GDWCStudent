using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pipe))]
public class PipeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Pipe pipe = (Pipe)target;
        if(GUILayout.Button("Update Pipe"))
        {
            pipe.UpdatePipe();
        }
    }
}
