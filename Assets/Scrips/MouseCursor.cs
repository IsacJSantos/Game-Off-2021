using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{ 
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private SpriteRenderer spriteRenderer;
    public Sprite[] cursor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;

    }


    void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPosition;
       /* if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeCursor();
            Debug.Log("Change cursor");
        }*/

    }

    public void ChangeCursor()
    {
        spriteRenderer.sprite = cursor[1];
    }

    public void DefaultCursor()
    {
        spriteRenderer.sprite = cursor[0];
    }
}





