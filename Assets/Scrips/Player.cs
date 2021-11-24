using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAgentTarget
{
    [SerializeField] Transform _bodyTransform;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody _bodyRb;
    [SerializeField] Collider _bodyCollider;
    Vector3 movement;  
    float addAngle = 270.0f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletOut;
    [SerializeField] float bulletSpeed;
    
    private void Update()
    {
        ProcessInputs();

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

        //https://www.youtube.com/watch?v=qMRrRQ587qQ
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletOut.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            bullet.GetComponent<Rigidbody>().velocity = Vector3.left * bulletSpeed;
        }
    }
   
    private void Move()
    {
        //movimentar personagem
        _bodyRb.velocity = movement;
    }

    public Vector3 GetClosestPoint(Vector3 objectPos)
    {
        return _bodyCollider.ClosestPointOnBounds(objectPos);
    }
}
