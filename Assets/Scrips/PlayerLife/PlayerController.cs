using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IAgentTarget, IBeatable
{
    [SerializeField] Transform _bodyTransform;
    [SerializeField] float _speed;
    float _defaultSpeed;
    [SerializeField] Rigidbody _bodyRb;
    [SerializeField] Collider _bodyCollider;
    Vector3 movement;
    float addAngle = 270.0f;
    [SerializeField] PlayerLife playerLife;
    [SerializeField] BaseLifeSystem baseLifeSystem;

    [SerializeField] float _runSpeed;
    [SerializeField] LifeBar lifeBar;
    bool _canMove = true;
    [SerializeField] public GameObject takeDamageParticle;
    [SerializeField] public GameObject healingParticle;
    [SerializeField] AudioSource takeTamageSound;

    private void Awake()
    {
        _defaultSpeed = _speed;
        Events.OnHealingPlayer += HealPlayer;
        Events.OnFireSuperShot += OnFireSuperShot;
        Events.OnEndWave += ResetPlayreLife;
    }

    void Start()
    {
       // lifeBar.SetMaxLife(playerLife.MaxLife);
    }
   
    private void OnDestroy()
    {
        Events.OnHealingPlayer -= HealPlayer;
        Events.OnFireSuperShot -= OnFireSuperShot;
        Events.OnEndWave -= ResetPlayreLife;
    }
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

        IsRunning();
    }

    private void Move()
    {
        //movimentar personagem

        if (movement == Vector3.zero || !_canMove) return;

        _bodyRb.velocity = movement;
    }

    private void IsRunning()
    {
        //aumenta a velocidade do personagem
        //se o jogador mantiver pressionada a tecla de correr

        if (Input.GetButton("Run"))
        {
            _speed = _runSpeed;
        }
        else if (Input.GetButtonUp("Run")) {
            _speed = _defaultSpeed;
        }
    }

    public Vector3 GetClosestPoint(Vector3 objectPos)
    {
        return _bodyCollider.ClosestPointOnBounds(objectPos);
    }
    public void ResetPlayreLife()
    {
        playerLife.Life = playerLife.MaxLife;
    }
    public void Hit(float value)
    {
        playerLife.Life -= value;

       // lifeBar.SetLife(baseLifeSystem.Life);

        if (!playerLife.IsAlive)
            Events.OnPlayerDie?.Invoke();


        TakeDamage();
    }

    void HealPlayer(float percent)
    {
        if (playerLife.IsAlive)
        {
            playerLife.Life += playerLife.MaxLife * (percent / 100);
            PlayHealingParticle();
        }        
    }

    void OnFireSuperShot()
    {
        StartCoroutine(PlayerMoveStum());
        _bodyRb.AddForce((_bodyRb.transform.forward * -1) * 8000 * Time.deltaTime, ForceMode.VelocityChange);
    }

    IEnumerator PlayerMoveStum()
    {
        _canMove = false;
        yield return new WaitForSeconds(0.5f);
        _canMove = true;
    }

    public void PlayHealingParticle()
    {
        ParticleSystem particle = healingParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }

    public void PlayTakeDamageParticle()
    {
        ParticleSystem particle = takeDamageParticle.GetComponent<ParticleSystem>();
        particle.Play();
    }

    public void TakeDamage()
    {
        if (takeDamageParticle)
        {
            PlayTakeDamageParticle();
        }

        if (takeTamageSound)
        {
            takeTamageSound.PlayOneShot(takeTamageSound.clip, 1.0f);
        }
    }
}
