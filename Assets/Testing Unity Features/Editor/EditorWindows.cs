using System;
using UnityEditor;
using UnityEngine;

public class EditorWindows : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    Color myColor = Color.white;

    [MenuItem("Editor Window Test/Editor Window")]
    private static void MyCustomAction()
    {
        GetWindow<EditorWindows>("Custom Editor Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Label("Color Picker", EditorStyles.boldLabel);
        myColor = EditorGUILayout.ColorField("Color Field", myColor);

        if (GUILayout.Button("Apply Color"))
        {
            // Apply color logic
            Debug.Log("Color applied: " + myColor);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Window Closed");
    }
}
