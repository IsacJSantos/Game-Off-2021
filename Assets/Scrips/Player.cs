using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector3 movement;
    [SerializeField] float speed;
    [SerializeField] float addAngle = 270.0f;

    private void Update()
    {
        ProcessInputs();
        //AimAndShoot();
        Move();
    }

    private void ProcessInputs()
    {
        //coletar inputs para movimento do personagem
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        //alinhar a rotação do personagem conforme a mira
        //https://www.youtube.com/watch?v=Geb_PnF1wOk&t=63s
        //https://stackoverflow.com/questions/59234157/make-character-look-at-mouse-in-unity

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 0.0f, transform.position.z));
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = angle + addAngle;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private void Move()
    {
        //movimentar personagem
        transform.position = transform.position + movement * Time.deltaTime * speed;
    }
}
