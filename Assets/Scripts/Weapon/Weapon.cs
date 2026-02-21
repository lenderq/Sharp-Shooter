using Unity.Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;

    ParticleSystem muzzleFlash;
    CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.hitVFXPrefab, hit.point, Quaternion.identity);


            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }

    }
}
