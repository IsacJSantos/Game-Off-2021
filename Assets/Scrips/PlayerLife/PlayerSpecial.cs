using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] SpecialType _specialType;
    [SerializeField] GameObject _bombPrefab;
    [SerializeField] Transform bulletOut;
    [SerializeField] Transform _bodyTransform;

    [SerializeField] Image _HUDImage;

    [Header("Values")]
    [SerializeField] float _healingPercent;
    [SerializeField] AudioSource _healing1;
    [SerializeField] AudioSource _superShot1;

    float delay;
    Coroutine coroutine;
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

            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(UpdateHud());


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
    IEnumerator UpdateHud()
    {
        _HUDImage.fillAmount = 1;
        float x = 1 / _cooldown;
        while (Time.time < delay)
        {
            _HUDImage.fillAmount -= x * Time.deltaTime;
            yield return null;
        }
        _HUDImage.fillAmount = 0;
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
        _cooldown -= 0.8f;
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
