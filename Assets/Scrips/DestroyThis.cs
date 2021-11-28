using System.Collections;
using UnityEngine;

//destroi gameobject apos colisao ou tempo
public class DestroyThis : MonoBehaviour
{

    [SerializeField] string _ignoretag;

    [SerializeField] float destroyAfterTimeDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _ignoretag) return;

        Destroy(gameObject);
    }

    private void Start()
    {
        IEnumerator destroyGameObject = DestroyGameObject(destroyAfterTimeDelay);
        StartCoroutine(destroyGameObject);
    }

    private IEnumerator DestroyGameObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
