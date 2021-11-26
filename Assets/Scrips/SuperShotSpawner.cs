using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShotSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    private void Awake()
    {
        Events.OnFireSuperShot += Fire;
    }

    private void OnDestroy()
    {
        Events.OnFireSuperShot -= Fire;
    }
    void Fire()
    {
        Quaternion angle = Quaternion.Euler(0,transform.parent.rotation.eulerAngles.y + transform.localRotation.eulerAngles.y, 0) ;
        Instantiate(_prefab, transform.position,angle);
    }
}
