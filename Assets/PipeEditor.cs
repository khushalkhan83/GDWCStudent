using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pipe))]
public class PipeEditor : Editor
{
    Pipe pipe;
    private void Awake()
    {
        pipe = (Pipe)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Pipe"))
        {
            pipe.UpdatePipe();
        }
    }
}
