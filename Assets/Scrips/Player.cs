using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 3f;

    private Rigidbody2D rb2d;
    Vector2 movement;

    Vector2 mousePos;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.Translate(movement * Time.deltaTime * speed);
        /*  var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
         var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/


    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }

}
