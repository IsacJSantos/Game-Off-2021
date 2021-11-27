using System.Collections;
using UnityEngine;

public class DestroyThis : MonoBehaviour
//destroi gameobject apos colisao ou tempo

{
    [SerializeField] float destroyAfterTimeDelay;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        IEnumerator destroyGameObject = DestroyGameObject(destroyAfterTimeDelay);
        StartCoroutine(destroyGameObject);
    }

    private IEnumerator DestroyGameObject(float delay)
    {
        Debug.Log(transform + "destroy");
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
