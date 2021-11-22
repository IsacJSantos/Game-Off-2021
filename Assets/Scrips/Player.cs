using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 movement;
    [SerializeField] float speed;
    float addAngle = 270.0f;

    private void Update()
    {
        ProcessInputs();
        //AimAndShoot();
        Move();
    }

    private void ProcessInputs()
    {
        //coletar inputs do movimento do personagem
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        movement = Vector3.ClampMagnitude(movement, 1.0f); //prevenir bug da velocidade maior na diagonal

        //coletar inputs da mira
        Vector3 aimPos = Input.mousePosition - Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 0.0f, transform.position.z));
        float angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        angle = angle + addAngle;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
    }

    private void Move()
    {
        //movimentar personagem
        transform.position = transform.position + movement * Time.deltaTime * speed;
    }
}
