using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SaveInstance : Editor
{
    [MenuItem("Instances/Create New Instance")]
    private static void SaveNewInstance()
    {
        if (SaveInstanceScriptable.LoadedInstance == null)
        {
            var instance = CreateInstance<SaveInstanceScriptable>();

            string path = Path.Combine(Application.dataPath, "Resources");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string str = Path.Combine(Path.Combine("Assets", "Resources"), $"{nameof(SaveInstanceScriptable)}.asset");
            
            AssetDatabase.CreateAsset(instance, str);
        }
        Selection.activeObject = SaveInstanceScriptable.Instance;
    }

    [MenuItem("Instances/Get Value From Instance")]
    private static void GetValue()
    {
        if (SaveInstanceScriptable.LoadedInstance == null)
        {
            Debug.Log("Instance has not been created...");
            return;
        }
        Debug.Log(SaveInstanceScriptable.Instance.value);
    }
}
