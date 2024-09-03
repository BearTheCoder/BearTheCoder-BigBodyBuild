using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAndClickController : MonoBehaviour
{
    #region Private Variables
    private bool _hovering;
    #endregion

    private void Start()
    {
        _hovering = false;
    }

    private void OnMouseOver()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        _hovering = true;
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(.5f, .5f, 0);
        _hovering = false;
    }
    
    private void Update()
    {
        if (!_hovering) return;
        if (!Input.GetMouseButton(0)) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }
}
