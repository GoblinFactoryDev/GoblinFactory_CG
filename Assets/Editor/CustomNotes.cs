using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeveloperNotes))]
public class CustomNotes : Editor
{
    public override void OnInspectorGUI()
    {
        DeveloperNotes scriptReference = (DeveloperNotes)target; // script reference
        DrawDefaultInspector();
        if (GUILayout.Button("Create Notes"))
        {
            scriptReference.MakeNote();
        }
    }
}
