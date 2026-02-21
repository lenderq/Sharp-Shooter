using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] int damage = 2;
    [SerializeField] float fireRate = 3f;

    PlayerHealth player;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        if (!player.IsDie)
            turretHead.LookAt(playerTargetPoint.position);
    }

    private IEnumerator FireRoutine()
    {
        while (!player.IsDie || this.enabled)
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, turretHead.rotation).GetComponent<Projectile>();
            projectile.Init(damage);
            projectile.transform.LookAt(playerTargetPoint.position);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
