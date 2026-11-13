using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float damage;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public override void Use()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, attackDistance))
        {
            /*
            if (hit.transform.TryGetComponent(out EnemyBone enemyBone))
            {
                enemyBone.TakeDamage(damage);
            }
            */
        }
    }
}
