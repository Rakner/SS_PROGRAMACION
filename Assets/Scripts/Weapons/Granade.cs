using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class Granade : MonoBehaviour
{
    [SerializeField] private float impulseForce;
    [SerializeField] private float timeForRelease;
    
    public ObjectPool<Granade> MyPool { get; set; }
    
    
    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * impulseForce, ForceMode.Impulse);
        StartCoroutine(WaitAndRelease());
    }

    private IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(timeForRelease);
        MyPool.Release(this);
        
    }
}
