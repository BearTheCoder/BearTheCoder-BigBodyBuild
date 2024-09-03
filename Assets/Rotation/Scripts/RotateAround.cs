using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] GameObject Sun;
    [SerializeField] GameObject Moon;
    #endregion
    
    #region Public Variables
    
    #endregion

    #region Private Variables
    
    #endregion

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!Input.GetKey(KeyCode.W)) return;
        Moon.transform.RotateAround(Sun.transform.position, Vector3.back, 1f);
    }

    private void OnDisable()
    {
        // Unsubscribe from events here.
    }
}
