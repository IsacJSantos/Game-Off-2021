using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] SpecialType specialType;
    [SerializeField] GameObject _bombPrefab;
    [SerializeField] GameObject _specialBulletPrefab;
    [SerializeField] Transform bulletOut;

    [Header("Values")]
    [SerializeField] float _healingPercent;

    float time;
    void Start()
    {

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
        if (Time.time >= time + _cooldown)
        {
            print("Special Attack!!!");
            time = Time.time;

            switch (specialType)
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
    }
    void Pump() 
    {
        print("Bomb");
        Instantiate(_bombPrefab, bulletOut.position, Quaternion.identity);
    }
    void SuperShot() 
    {
        print("SuperShot");
    }
}
//[System.Serializable]
public enum SpecialType
{
    Healing,
    Bomb,
    SuperShot
}
