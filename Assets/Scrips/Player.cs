using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _bodyTransform;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _bodyRb;

    Vector3 movement;  
    float addAngle = 270.0f;
    
    private void Update()
    {
        ProcessInputs();
     
        //AimAndShoot();

    }
    private void FixedUpdate()
    {
        Move();
    }
    private void ProcessInputs()
    {
        //coletar inputs do movimento do personagem
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        movement = Vector3.ClampMagnitude(movement, 1.0f) * _speed * Time.fixedDeltaTime; //prevenir bug da velocidade maior na diagonal
     
        //coletar inputs da mira
        Vector3 aimPos = Input.mousePosition - Camera.main.WorldToScreenPoint(new Vector3(_bodyTransform.position.x, 0.0f, _bodyTransform.position.z));
        float angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        angle = angle + addAngle;
        _bodyTransform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
       
    }
   
    private void Move()
    {
        //movimentar personagem
        _bodyRb.velocity = movement;
    }
}
