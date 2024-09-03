using System;
using UnityEngine;

public class SaveInstanceScriptable : ScriptableObject
{
    [SerializeField] internal string value = "butt";

    private static SaveInstanceScriptable _instance;

    public static SaveInstanceScriptable Instance 
    {
        get
        {
            _instance = LoadedInstance;
            return _instance ?? CreateInstance<SaveInstanceScriptable>();
        }
    }

    public static SaveInstanceScriptable LoadedInstance => _instance ?? Resources.Load(nameof(SaveInstanceScriptable)) as SaveInstanceScriptable;
}
