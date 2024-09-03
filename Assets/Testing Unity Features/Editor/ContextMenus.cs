using UnityEngine;

public class ContextMenus : MonoBehaviour
{
    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Performed operation...");
    }
}
