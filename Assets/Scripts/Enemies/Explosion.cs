using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    private void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Explode()
    {
        Collider[] overlapSpheres = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in overlapSpheres)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue;

            playerHealth.TakeDamage(damage);

            break;
        }
    }
}
