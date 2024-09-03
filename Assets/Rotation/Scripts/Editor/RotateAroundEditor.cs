using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotateAround))]
public class RotateAroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        GUILayout.Space(10);

        // Add a custom button
        if (GUILayout.Button("Open Docs"))
        {
            Application.OpenURL("https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html");
        }
    }
}