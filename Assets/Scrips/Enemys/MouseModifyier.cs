using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseModifyier : MonoBehaviour
{
   public MouseCursor newCursor;

    private void Start()
    {
        newCursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<MouseCursor>();
    }

    void OnMouseOver()
    {
        // If the mouse hover over the GameObject with the script attached
        Debug.Log("Mouse is hovering enemy");
        newCursor.ChangeCursor();
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject
        Debug.Log("Mouse is no longer on enemy");
        newCursor.DefaultCursor();
    }
}
