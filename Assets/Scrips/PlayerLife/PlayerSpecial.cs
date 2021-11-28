using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] SpecialType _specialType;
    [SerializeField] GameObject _bombPrefab;
    [SerializeField] Transform bulletOut;
    [SerializeField] Transform _bodyTransform;

    [Header("Values")]
    [SerializeField] float _healingPercent;
    [SerializeField] AudioSource _healing1;
    [SerializeField] AudioSource _throwBomb1;
    [SerializeField] AudioSource _superShot1;


    float delay;

    private void Awake()
    {
        Events.OnDecreaseAbilityCooldown += DecreaseAbilityCooldown;
        Events.OnChooseSpecial += ChooseSpecial;
    }
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        Events.OnDecreaseAbilityCooldown -= DecreaseAbilityCooldown;
        Events.OnChooseSpecial -= ChooseSpecial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoSpecial();
        }
    }

    void DoSpecial()
    {
        if (Time.time >= delay)
        {
            delay = Time.time + _cooldown;
            print("Special Attack!!!");     

            switch (_specialType)
            {
                case SpecialType.Healing:
                    Healing();
                    break;
                case SpecialType.Bomb:
                    Pump();
                    break;
                case SpecialType.SuperShot:
                    SuperShot();
                    break;
                default:
                    break;
            }
        }
    }

    void Healing()
    {
        print("Healing");
        Events.OnHealingPlayer?.Invoke(_healingPercent);

        //sound
        if (_healing1.clip)
        {
            _healing1.PlayOneShot(_healing1.clip, 1.0f);
        }
    }
    void Pump() 
    {
        print("Bomb");
        Instantiate(_bombPrefab, bulletOut.position, Quaternion.Euler(0, _bodyTransform.rotation.eulerAngles.y, 0));
        
        //sound
        if (_throwBomb1.clip)
        {
            _throwBomb1.PlayOneShot(_throwBomb1.clip, 1.0f);
        }
    }
    void SuperShot() 
    {
        print("SuperShot");
        Events.OnFireSuperShot?.Invoke();

        //sound
        if (_superShot1.clip)
        {
            _superShot1.PlayOneShot(_superShot1.clip, 1.0f);
        }
    }

    void DecreaseAbilityCooldown() 
    {
        if (_cooldown <= 1) return;
        _cooldown -= 0.5f;
    }

    void ChooseSpecial(SpecialType specialType)
    {
        _specialType = specialType;
    }
}
//[System.Serializable]
public enum SpecialType
{
    Healing,
    Bomb,
    SuperShot
}
