using UnityEngine;
using UnityEngine.Pool;

public class Cluster : Weapon
{
    private ObjectPool<Granade> granadePool;
    [SerializeField] private Granade granadePrefab;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        granadePool = new ObjectPool<Granade>(OnCreateGranade, OnGetGranade, OnReleaseGranade);
    }

    private Granade OnCreateGranade()
    {
        Granade granadeCopy = Instantiate(granadePrefab, spawnPoint.position, spawnPoint.rotation);
        granadeCopy.MyPool = granadePool;
        return granadeCopy;
    }

    private void OnGetGranade(Granade obj)
    {
        obj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        obj.transform.position = spawnPoint.position;
        obj.transform.rotation = spawnPoint.rotation;
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseGranade(Granade obj)
    {
        obj.gameObject.SetActive(false);
    }

    public override void Use()
    {
        granadePool.Get();
    }
}
